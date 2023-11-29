using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class FoodDashboardDto
    {
        public FoodDashboardDto()
        {
            this.menuList = new List<MenuDto>();
        }
        public List<MenuDto> menuList { get; set; }
    }
}
