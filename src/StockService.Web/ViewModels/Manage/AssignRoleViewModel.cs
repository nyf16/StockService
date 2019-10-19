using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.Web.ViewModels.Manage
{
    public class AssignRoleViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DisplayName("İşlem yapmak istediğiniz yetkiyi seçin")]
        public string RoleId { get; set; }
        public List<SelectListItem> RoleList { get; set; }
    }
}
