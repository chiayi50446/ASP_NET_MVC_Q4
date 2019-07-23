using MVC_Q4.Models.Interface;
using MVC_Q4.Models.Repository;
using MVC_Q4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Q4.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentRepository departmentRepository;
        private DepartmentViewModel departmentData;
        public DepartmentController()
        {
            departmentRepository = new DepartmentRepository();
            departmentData = new DepartmentViewModel();
        }
        // GET: Department
        public ActionResult Index()
        {
            departmentData.DropDown1 = SetDropDown1();
            departmentData.DropDown2 = SetDropDown2();
            return View(departmentData);
        }

        [HttpPost]
        public ActionResult Index(DepartmentViewModel data)
        {
            departmentData.Department = departmentRepository.GetDepartmentById(data.Department.Id) ?? new Department { Name = "Do not choose any department"};
            departmentData.SubDepartment = departmentRepository.GetSubDepartmentById(data.SubDepartment.Id) ?? new SubDepartment { Name = "Do not choose any sub-department" };
            departmentData.DropDown1 = SetDropDown1();
            departmentData.DropDown2 = SetDropDown2(data.Department.Id);
            return View(departmentData);
        }

        // 初始化DropDownList      
        public List<SelectListItem> GetSelectItem(bool dvalue = true)
        {
            List<SelectListItem> items = new List<SelectListItem> ();
            if (dvalue) { items.Insert(0, new SelectListItem { Text = "--請選擇--", Value = "0" }); }
            return items;
        }

        // 第一層下拉選項       
        public List<SelectListItem> SetDropDown1()
        {
            List<SelectListItem> items = GetSelectItem();
            List<Department> departments = departmentRepository.GetDepartmentLst();
            items.AddRange( departments.Select(s => new SelectListItem()
            {
                Text = s.Id + " : " + s.Name,
                Value = s.Id,
            }).ToList() );
            return items;
        }

        // 第二層下拉選項       
        public List<SelectListItem> SetDropDown2(string id = "")
        {
            List<SelectListItem> items = GetSelectItem();
            if (id != "")
            {
                List<SubDepartment> subDepartments = departmentRepository.GetSubDepartmentList().Where(x => x.ParentId == id).ToList();
                items.AddRange(subDepartments.Select(s => new SelectListItem()
                {
                    Text = s.Id + " : " + s.Name,
                    Value = s.Id,
                }).ToList());
            }
            return items;
        }

        public JsonResult GetSubDepart(string id)
        {
            return Json(SetDropDown2(id));
        }

    }
}