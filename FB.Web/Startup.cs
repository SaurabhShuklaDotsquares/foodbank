using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Dto.Foodbank;
using FB.Repo;
using FB.Service;
using FB.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.RegularExpressions;

namespace FB.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            });
            services.AddSession();
            services.AddMvc();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<MMOOlgaDevContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().AddFluentValidation();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddHttpClient();
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddNewtonsoftJson();
            services.AddDistributedMemoryCache();

            SiteKeys.Configure(Configuration.GetSection("SiteKeys"));
            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.LoginPath = "/Account/Login";
            });
            services.Configure<FormOptions>(x => x.ValueCountLimit = 1048576);
            services.AddRazorPages().AddRazorRuntimeCompilation();
            InitServices(services);
            InitValidation(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Index");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            MMOHttpContext.Services = app.ApplicationServices;
            app.UseAuthentication();
            app.UseCookiePolicy(
           new CookiePolicyOptions
           {
               Secure = CookieSecurePolicy.Always
           });

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    Regex rgx = new Regex("[^\\s]+(.*?)\\.(jpg|JPG|jpeg|JPEG|gif|GIF|png|PNG|bmp|BMP|pdf|PDF|doc|DOC|docx|DOCX|xls|XLS|xlsx|XLSX|csv|CSV|txt|TXT|rtf|RTF|html|html|zip|ZIP)$");
                    if (rgx.IsMatch(ctx.Context.Request.Path) && (ctx.Context.Request.Path.StartsWithSegments("/MemberPhotos") || ctx.Context.Request.Path.StartsWithSegments("/FamilyPhotos") || ctx.Context.Request.Path.StartsWithSegments("/Correspondence")))
                    {
                        ctx.Context.Response.Headers.Add("Cache-Control", "no-store");
                        if (!ctx.Context.User.Identity.IsAuthenticated)
                        {
                            ctx.Context.Response.Redirect(SiteKeys.DomainName + "Error/AccessDenied");
                        }
                        else
                        {
                            var request = ctx.Context.Request;
                            if (ctx.Context.Request.Path.StartsWithSegments("/MemberPhotos"))
                            {
                                UriBuilder uriBuilder = new UriBuilder
                                {
                                    Scheme = request.Scheme,
                                    Host = request.Host.Host,
                                    Path = string.Format("{0}{1}", request.PathBase, request.Path.ToString().ToLower().Replace("/memberphotos/", "/member/accesspersonphoto/")).Trim(),
                                    Query = request.QueryString.ToString()
                                };
                                ctx.Context.Response.Redirect(uriBuilder.Uri.ToString());
                            }

                            if (ctx.Context.Request.Path.StartsWithSegments("/FamilyPhotos"))
                            {
                                UriBuilder uriBuilder = new UriBuilder
                                {
                                    Scheme = request.Scheme,
                                    Host = request.Host.Host,
                                    Path = string.Format("{0}{1}", request.PathBase, request.Path.ToString().ToLower().Replace("/familyphotos/", "/member/accessfamilyphoto/")).Trim(),
                                    Query = request.QueryString.ToString()
                                };
                                ctx.Context.Response.Redirect(uriBuilder.Uri.ToString());

                            }

                            if (ctx.Context.Request.Path.StartsWithSegments("/Correspondence"))
                            {
                                UriBuilder uriBuilder = new UriBuilder
                                {
                                    Scheme = request.Scheme,
                                    Host = request.Host.Host,
                                    Path = string.Format("{0}{1}", request.PathBase, request.Path.ToString().ToLower().Replace("/correspondence/", "/correspondence/accesscorrespondence/")).Trim(),
                                    Query = request.QueryString.ToString()
                                };
                                ctx.Context.Response.Redirect(uriBuilder.Uri.ToString());

                            }
                        }
                    }
                }
            });

            app.UseMvc(routes =>
            {
                

                routes.MapAreaRoute(
                    name: "FoodBank",
                    areaName: "FoodBank",
                    template: "FoodBank/{controller=Home}/{action=Index}/{id?}"
                    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );

                routes.MapAreaRoute(
                   name: "Report",
                   areaName: "Report",
                   template: "Report/{controller=ReferrerReport}/{action=Index}/{id?}"
                   );
            });

            ContextProvider.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>(), env);
        }

        public void InitServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ICentralOfficeService, CentralOfficeService>();
            services.AddScoped<ICharityService, CharityService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IUserPreferenceService, UserPreferenceService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IQuickDonorGiftService, QuickDonorGiftService>();
            services.AddScoped<IDonorService, DonorService>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IMyReferralService, MyReferralService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IAllergiesService, AllergiesService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IFoodbankService, FoodbankService>();
            services.AddScoped<IGrantorService, GrantorService>();
            services.AddScoped<IAgenciesService, AgenciesService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<IParcelService, ParcelService>();
            services.AddScoped<IFamilyParcelService, FamilyParcelService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IProfessionServices, ProfessionServices>();
            services.AddScoped<IFoodbankDataAccessibilityService, FoodbankDataAccessibilityService>();
        }

        /// <summary>
        /// Initializes the validation.
        /// </summary>
        /// <param name="services">The services.</param>
        public void InitValidation(IServiceCollection services)
        {
            services.AddTransient<IValidator<PersonDto>, PersonDtoValidator>();
            services.AddTransient<IValidator<PersonAddressDto>, PersonAddressDtoValidator>();
            services.AddTransient<IValidator<EditPersonDto>, EditPersonDtoValidator>();
            services.AddTransient<IValidator<DonorDonationDto>, DonorDonationDtoValidator>();
            services.AddTransient<IValidator<DeclarationDto>, DeclarationDtoValidator>();
            services.AddTransient<IValidator<FamilyDTo>, familydtovalidation>();

            services.AddTransient<IValidator<ReferrerProfileDto>, ReferrerProfileDtoValidator>();
            services.AddTransient<IValidator<VolunteerDto>, VolunteerProfileDtoValidator>();
            services.AddTransient<IValidator<AvailabilityDto>, VolunteerAvailabilityDtoValidator>();
            services.AddTransient<IValidator<UnavailabilityDto>, VolunteerUnAvailabilityDtoValidator>();
            services.AddTransient<IValidator<AddFamilyDto>, AddFamilyDtoValidation>();
            services.AddTransient<IValidator<ReferrerRegisterDto>, ReferrerRegisterDtoValidator>();
            services.AddTransient<IValidator<VolunteerRegisterDto>, VolunteerRegisterDtoValidator>();
            services.AddTransient<IValidator<ForgotPasswordDto>, ForgotPasswordDtoValidator>();
            services.AddTransient<IValidator<LoginViewDto>, LoginViewDtoValidator>();
            services.AddTransient<IValidator<FeedbackMasterDTO>, FeedbackMasterDtoValidator>();
            services.AddTransient<IValidator<UpdateProfileDto>, FoodbankUpdateProfileDtoValidator>();
            services.AddTransient<IValidator<VolunteerAdminDto>, VolunteerProfileAdminDtoValidator>();
            services.AddTransient<IValidator<GrantorDto>, GrantorDtoValidator>();
            services.AddTransient<IValidator<AgenciesDto>, AgencieDtoValidator>();
            services.AddTransient<IValidator<StockDto>, AddStockDtoValidation>();
            services.AddTransient<IValidator<UserDto>, UserDtoValidator>();
            services.AddTransient<IValidator<FoodDonationDto>, AddFoodDonationDtoValidator>();
            services.AddTransient<IValidator<FoodbankSettingDto>, FoodbankSettingDtoValidator>();
            services.AddTransient<IValidator<FoodbankRecipeDto>, FoodbankRecipeDtoValidator>();
            services.AddTransient<IValidator<EditFamilyDto>, EditFamilyDtoValidation>();
            services.AddTransient<IValidator<ParcelTypeDto>, ParcelTypeDtoValidator>();
            services.AddTransient<IValidator<FamilyParcelDto>, FamilyParcelDtoValidator>();
            services.AddTransient<IValidator<AdminEditFamilyDto>, AdminEditFamilyDtoValidation>();
            services.AddTransient<IValidator<VoucherDto>, VoucherDtoValidator>();
            services.AddTransient<IValidator<StockFoodDonationDto>, AddFoodDonationInStockDtoValidator>();
            services.AddTransient<IValidator<RoleDto>, RoleDtoValidator>();
        }
    }
}
