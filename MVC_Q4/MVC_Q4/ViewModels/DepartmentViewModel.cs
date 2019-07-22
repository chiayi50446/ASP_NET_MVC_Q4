using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Q4.ViewModels
{
    public class DepartmentViewModel
    {
        public Department Department { get; set; }
        public List<SelectListItem> DropDown1 { get; set; }
        public SubDepartment SubDepartment { get; set; }
        public List<SelectListItem> DropDown2 { get; set; }
    }

    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SubDepartment
    {
        public string ParentId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}