using System.Linq;
using FB.Repo;
using FB.Data.Models;
using System.Collections.Generic;
using FB.Dto;
using FB.ModalMapper;
using FB.Core;
using System.Linq.Expressions;
using System;

namespace FB.Service
{
    public class RoleService : IRoleService
    {
        private IRepository<Role> repoRole;
        private IRepository<UserRole> repoUserRole;
        private IRepository<FoodbankMenu> repoFoodbankMenu;
        private IRepository<FoodbankRoleMenuPrivilege> repoFoodbankRoleMenuPrivilege;


        public RoleService(IRepository<Role> _repoRole, IRepository<UserRole> _repoUserRole, IRepository<FoodbankMenu> _repoFoodbankMenu,
            IRepository<FoodbankRoleMenuPrivilege> _repoFoodbankRoleMenuPrivilege)
        {
            this.repoRole = _repoRole;
            this.repoUserRole = _repoUserRole;
            repoFoodbankMenu = _repoFoodbankMenu;
            repoFoodbankRoleMenuPrivilege = _repoFoodbankRoleMenuPrivilege;
        }

        public Role GetRoleByName(string name)
        {
            return repoRole.Query().Filter(r => r.RoleName == name).Get().FirstOrDefault();
        }
        public Role GetRoleById(int id)
        {
            return repoRole.Query().Filter(r => r.RoleId == id && r.IsFoodbankRole==true).Include(ur => ur.UserRole).Get().FirstOrDefault();
            //return repoRole.FindById(id);
        }
        public Role GetRoleByIdandFoodbank(int id, int FoodbankId)
        {
            return repoRole.Query().Filter(r => r.RoleId == id && r.FoodbankId== FoodbankId && r.IsFoodbankRole == true).Include(ur => ur.UserRole).Get().FirstOrDefault();
            //return repoRole.FindById(id);
        }

        /// <summary>
        /// To get listing of roles
        /// </summary>
        /// <returns></returns>
        public List<RoleDto> GetRoles()
        {
            var rolelist = repoRole.Query()
                .Filter(r => r.RoleName.ToLower() != "superadmin" && r.RoleName.ToLower() != "donor" && r.RoleName.ToLower() != "agent")
                .Get().ToList();

            return RoleDtoMapper.Map(rolelist);

        }

        /// <summary>
        /// To get single entity of role by role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleDto GetRole(int id)
        {
            var role = repoRole.Query().Filter(r => r.RoleId == id && r.IsFoodbankRole==true).Include(r => r.FoodbankRoleMenuPrivilege).Get().FirstOrDefault();
            return RoleDtoMapper.Map(role);
        }
        public RoleDto GetRoleByFoodbank(int id,int FoodbankId)
        {
            var role = repoRole.Query().Filter(r => r.RoleId == id && r.FoodbankId== FoodbankId && r.IsFoodbankRole == true).Include(r => r.FoodbankRoleMenuPrivilege).Get().FirstOrDefault();
            return RoleDtoMapper.Map(role);
        }


        public void Save(UserRole userRole, bool isNew = true)
        {
            if (isNew)
            {

                repoUserRole.InsertGraph(userRole);
            }
            else
            {
                repoUserRole.Update(userRole);
            }
        }

        /// <summary>
        /// To save role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="isNew"></param>
        public void Save(Role role, bool isNew = true)
        {
            if (isNew)
            {
                repoRole.InsertGraph(role);
            }
            else
            {
                repoRole.SafeAttach(role, b => b.RoleId);
                repoRole.Update(role);
            }
        }
        public void Save(FoodbankRoleMenuPrivilege foodbankRoleMenuPrivilege, bool isNew = true)
        {
            if (isNew)
            {

                repoFoodbankRoleMenuPrivilege.InsertGraph(foodbankRoleMenuPrivilege);
            }
            else
            {
                repoFoodbankRoleMenuPrivilege.Update(foodbankRoleMenuPrivilege);
            }
        }
        public List<RoleDto> GetRolesByFoodbank(int? FoodbankId)
        {
            if (FoodbankId > 0)
            {
                var rolelist = repoRole.Query().Filter(c => c.FoodbankId == FoodbankId)
               .Get().ToList();
                return RoleDtoMapper.Map(rolelist);
            }
            else
            {
                var rolelist = repoRole.Query().Filter(c => c.CentralOfficeId == null && c.ParentRoleId == null &&
                (
                    c.RoleName.ToLower() == UserRoles.FoodbankStaff.ToString().ToLower()

                 )).Get().ToList();
                return RoleDtoMapper.Map(rolelist);
            }


        }

        /// <summary>
        /// To get roles by organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public List<RoleDto> GetRolesByOrganization(int? foodbankId)
        {
            if (foodbankId > 0)
            {
                //  var rolelist = repoRole.Query().Filter(c => c.RoleId == organizationId)
                //.Get().ToList();
                //  return RoleDtoMapper.Map(rolelist);

                var rolelist = repoRole.Query().Filter(c =>  c.FoodbankId == foodbankId && c.IsFoodbankRole==true)
                               .Get().ToList();
                return RoleDtoMapper.Map(rolelist);
            }
            else
            {
                var rolelist = repoRole.Query().Filter(c => c.CentralOfficeId == null && c.ParentRoleId == null &&
            (
                c.RoleName.ToLower() == UserRoles.Foodbank.ToString().ToLower() ||
                c.RoleName.ToLower() == UserRoles.FoodbankStaff.ToString().ToLower()
                ))
                           .Get().ToList();
            return RoleDtoMapper.Map(rolelist);
            }


        }

        public void SaveFoodbankRolePrivileges(List<FoodbankRoleMenuPrivilegeDto> mmoRoleMenuPrivilege, int roleID)
        {
            var allMenus = repoFoodbankMenu.Query().Get().ToList();

            var roleMenusToRemove = repoFoodbankRoleMenuPrivilege.Query().Filter(r => r.RoleId == roleID).Get().ToList();
            var roleIds = repoRole.Query().Filter(e => e.ParentRoleId == roleID).Get().Select(e => e.RoleId).ToList();
            var roleChildMenus = repoFoodbankRoleMenuPrivilege.Query().Filter(r => roleIds.Contains(r.RoleId)).Get().ToList();

            var predicate = PredicateBuilder.True<FoodbankMenu>();

            UserRoles role = (UserRoles)roleID;
            Expression<Func<FoodbankMenu, bool>> roleCondition = p => true;
            switch (role)
            {
                case UserRoles.SuperAdmin:
                    roleCondition = p => p.IsSuperAdminPermission == true;
                    break;
                case UserRoles.Agent:
                    roleCondition = p => p.IsAgentPermission == true;
                    break;
                case UserRoles.Organisation:
                    roleCondition = p => p.IsOrganizationPermission == true;
                    break;
                case UserRoles.Charity:
                    roleCondition = p => p.IsCharityPermission == true;
                    break;
                case UserRoles.Branch:
                    roleCondition = p => p.IsBranchPermission == true;
                    break;
                case UserRoles.Donor:
                    roleCondition = p => p.IsDonorPermission == true;
                    break;
                case UserRoles.Internal:
                    roleCondition = p => p.IsInternalPermission == true;
                    break;
                case UserRoles.TechnicalSupport:
                    roleCondition = p => p.IsTechnicalPermission == true;
                    break;
                case UserRoles.Input:
                    roleCondition = p => p.IsInputPermission == true;
                    break;
                default:
                    break;
            }

            roleMenusToRemove.ForEach(rmp =>
            {
                repoFoodbankRoleMenuPrivilege.Delete(rmp);
            });

            var roleMenuPrivilegeList = new List<FoodbankRoleMenuPrivilege>();

            foreach (var item in mmoRoleMenuPrivilege)
            {
                roleMenuPrivilegeList.Add(new FoodbankRoleMenuPrivilege
                {
                    MenuId = item.MenuID,
                    RoleId = item.RoleID
                });
                predicate = PredicateBuilder.True<FoodbankMenu>();
                predicate = predicate.And(p => p.ParentMenuId == item.MenuID && !p.ShowOnMenu && !p.ShowOnPermission);
                predicate = predicate.And(roleCondition);
                var menus = allMenus.AsQueryable().Where(predicate).ToList();
                foreach (var innerItem in menus)
                {
                    roleMenuPrivilegeList.Add(new FoodbankRoleMenuPrivilege
                    {
                        MenuId = innerItem.MenuId,
                        RoleId = item.RoleID
                    });
                }
            }

            if (roleMenuPrivilegeList.Count() > 0)
            {
                foreach (var roleMenuPrivilege in roleMenuPrivilegeList)
                {
                    Save(roleMenuPrivilege, true);
                }
            }


            if (roleIds.IsNotNullAndNotEmpty())
            {
                foreach (int childRoleID in roleIds)
                {
                    roleMenuPrivilegeList = new List<FoodbankRoleMenuPrivilege>();
                    var roleMenus1 = roleChildMenus.Where(r => r.RoleId == childRoleID).Select(e => e.MenuId);

                    foreach (int menuID in roleMenus1)
                    {
                        predicate = PredicateBuilder.True<FoodbankMenu>();
                        predicate = predicate.And(p => p.ParentMenuId == menuID && !p.ShowOnMenu && !p.ShowOnPermission);
                        predicate = predicate.And(roleCondition);

                        var menus = allMenus.AsQueryable().Where(predicate).ToList();
                        foreach (var innerItem in menus)
                        {
                            if (!roleMenus1.Contains(innerItem.MenuId))
                            {
                                roleMenuPrivilegeList.Add(new FoodbankRoleMenuPrivilege
                                {
                                    MenuId = innerItem.MenuId,
                                    RoleId = childRoleID
                                });
                            }
                        }
                    }

                    if (roleMenuPrivilegeList.Count() > 0)
                    {
                        foreach (var roleMenuPrivilege in roleMenuPrivilegeList)
                        {
                            Save(roleMenuPrivilege, true);
                        }
                    }
                }
            }

        }
        public void SaveRolePrivileges(int menuId, int roleID)
        {
            var roleMenusToRemove = repoFoodbankRoleMenuPrivilege.Query().Filter(r => r.RoleId == roleID && r.MenuId == menuId).Get().ToList();

            roleMenusToRemove.ForEach(rmp =>
            {
                repoFoodbankMenu.Delete(rmp);
            });

            repoFoodbankRoleMenuPrivilege.InsertGraph(new FoodbankRoleMenuPrivilege
            {
                MenuId = menuId,
                RoleId = roleID
            });
        }
        public void SaveRolePrivileges(List<FoodbankRoleMenuPrivilegeDto> roleMenuPrivilege, int roleID)
        {
            var allMenus = repoFoodbankMenu.Query().Get().ToList();
            var roleMenusToRemove = repoFoodbankRoleMenuPrivilege.Query().Filter(r => r.RoleId == roleID).Get().ToList();

            repoFoodbankRoleMenuPrivilege.DeleteCollection(roleMenusToRemove);

            var isCustomRole = IsCustomRole(roleID);

            var roleMenus = roleMenuPrivilege.Select(r => new FoodbankRoleMenuPrivilege
            {
                MenuId = r.MenuID,
                RoleId = r.RoleID,
                IsCreate = !isCustomRole,
                IsDelete = !isCustomRole,
                IsDetail = true,
                IsUpdate = !isCustomRole,
                IsList = true
            }).ToList();

            //get all hidden sub menu for selected record.
            var subRoleMenus = GetSubRoleMenus(isCustomRole, roleID, allMenus, roleMenus);

            if (subRoleMenus.Count > 0)
            {
                roleMenus.AddRange(subRoleMenus);
            }

            //get all read-only hidden menus to allow without any restriction
            var predicate = PredicateBuilder.True<FoodbankMenu>();
            //var actionTypes = new int[] { (int)MenuActionType.Detail, (int)MenuActionType.List, (int)MenuActionType.None }; // read-only menu items
            var idsIgnore = subRoleMenus.Select(s => s.MenuId).Distinct().ToArray();

            predicate = predicate.And(s => !s.ShowOnMenu && !s.ShowOnPermission && s.IsActive && !idsIgnore.Contains(s.MenuId));

            if (roleID == (int)UserRoles.SuperAdmin)
            {
                predicate = predicate.And(s => s.IsSuperAdminPermission == true);
            }
            else if (roleID == (int)UserRoles.Organisation || isCustomRole)
            {
                predicate = predicate.And(s => s.IsOrganizationPermission == true);
            }
            else if (roleID == (int)UserRoles.Foodbank)
            {
                predicate = predicate.And(s => s.IsOrganizationPermission == true);
            }
            else
            {
                predicate = predicate.And(s => s.IsOrganizationPermission == true);
            }

            var hiddenMenues = repoFoodbankMenu.Query().Filter(predicate).Get().ToList();
            if (hiddenMenues.Count > 0)
            {
                hiddenMenues.ForEach(m =>
                {
                    roleMenus.Add(new FoodbankRoleMenuPrivilege
                    {
                        MenuId = m.MenuId,
                        RoleId = roleID,
                        IsCreate = !isCustomRole,
                        IsDelete = !isCustomRole,
                        IsDetail = true,
                        IsUpdate = !isCustomRole,
                        IsList = true
                    });
                });
            }

            repoFoodbankRoleMenuPrivilege.InsertCollection(roleMenus);
        }
        public bool IsAccessibleRole(int[] roleIDs, int? organisationId = null)
        {
            return repoRole.Query().Filter(r => roleIDs.Contains(r.RoleId) && r.CentralOfficeId == organisationId).Get().Any();
        }

        private bool IsCustomRole(int roleId)
        {
            return !(roleId == (int)UserRoles.SuperAdmin || roleId == (int)UserRoles.Organisation || roleId == (int)UserRoles.Foodbank || roleId == (int)UserRoles.FoodbankStaff);
        }
        private List<FoodbankRoleMenuPrivilege> GetSubRoleMenus(bool isCustomRole, int roleID, List<FoodbankMenu> allMenus, List<FoodbankRoleMenuPrivilege> parentMenus)
        {
            var subMenus = allMenus.Where(m => parentMenus.Any(p => p.MenuId == m.ParentMenuId) && !m.ShowOnMenu && !m.ShowOnPermission);
            var subRoleMenus = subMenus
                .Select(r => new FoodbankRoleMenuPrivilege
                {
                    MenuId = r.MenuId,
                    RoleId = roleID
                }).ToList();

            if (roleID == (int)UserRoles.SuperAdmin)
            {
                subMenus = subMenus.Where(m => m.IsSuperAdminPermission == true);
            }
            else if (roleID == (int)UserRoles.Organisation || isCustomRole)
            {
                subMenus = subMenus.Where(m => m.IsOrganizationPermission == true);
            }
            else if (roleID == (int)UserRoles.Foodbank)
            {
                subMenus = subMenus.Where(m => m.IsOrganizationPermission == true);
            }
            else
            {
                subMenus = subMenus.Where(m => m.IsOrganizationPermission == true);
            }

            var subRoleMenusToAdd = subMenus
               .Select(r => new FoodbankRoleMenuPrivilege
               {
                   MenuId = r.MenuId,
                   RoleId = roleID,
                   IsCreate = !isCustomRole,
                   IsDelete = !isCustomRole,
                   IsDetail = true,
                   IsUpdate = !isCustomRole,
                   IsList = true
               }).ToList();

            if (subRoleMenus.Count > 0)
            {
                var subRoleMenus2 = GetSubRoleMenus(isCustomRole, roleID, allMenus, subRoleMenus);
                if (subRoleMenus2.Count > 0)
                {
                    subRoleMenusToAdd.AddRange(subRoleMenus2);
                }
            }

            return subRoleMenusToAdd;
        }

        public RoleDto GetRole(int id, int? organisationId = null, bool isAdmin = false)
        {
            var res = repoRole.Query().Filter(r => r.RoleId == id && (r.CentralOfficeId == organisationId || isAdmin)).Include(r => r.FoodbankRoleMenuPrivilege).Get().FirstOrDefault();
            return RoleDtoMapper.Map(res);
        }
        public RoleDto GetRolePrivileges(int id, int? organisationId = null, bool isAdmin = false)
        {
            var res = repoRole.Query().Filter(r => r.RoleId == id && (r.CentralOfficeId == organisationId || isAdmin)).Include(r => r.FoodbankRoleMenuPrivilege).Get().FirstOrDefault();
            return RoleDtoMapper.Map(res);
        }
        /// <summary>
        /// To get listing of role for server side data table
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public KeyValuePair<int, List<RoleDto>> GetRoles(DataTableServerSide searchModel, int? organisationId,int FoodbankId)
        {
            var predicate = CustomPredicate.BuildPredicate<Role>(searchModel);
            predicate = predicate.And(r => r.CentralOfficeId == organisationId);
            predicate = predicate.And(r => r.IsFoodbankRole == true);
            predicate = predicate.And(r => r.FoodbankId == FoodbankId);
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var res = repoRole
                 .Query()
                 .Filter(predicate)
                 .OrderBy(x => x.OrderByDescending(oo => oo.RoleId))
                 .CustomOrderBy(u => u.OrderBy(searchModel))
                 .GetPage(page, searchModel.length, out totalCount)
                 .ToList();

            var results = RoleDtoMapper.Map(res);
            return new KeyValuePair<int, List<RoleDto>>(totalCount > 0 ? totalCount : results.Count, results);
        }

        /// <summary>
        /// To delete the role
        /// </summary>
        /// <param name="id"></param>
        public void RoleDelete(int id)
        {
            repoRole.Delete(id);
        }

        public void Dispose()
        {
            if (repoRole != null)
            {
                repoRole.Dispose();
                repoRole = null;
            }
            if (repoUserRole != null)
            {
                repoUserRole.Dispose();
                repoUserRole = null;
            }
        }

        
    }
}
