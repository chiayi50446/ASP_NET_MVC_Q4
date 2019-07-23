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
        private IDepartmentRepository _departmentRepository;
        private DepartmentViewModel _departmentData;
        public DepartmentController()
        {
            _departmentRepository = new DepartmentRepository();
            _departmentData = new DepartmentViewModel();
        }
        // GET: Department
        public ActionResult Index()
        {
            _departmentData.DropDown1 = SetDropDown1();
            _departmentData.DropDown2 = SetDropDown2();
            return View(_departmentData);
        }

        [HttpPost]
        public ActionResult Index(DepartmentViewModel data)
        {
            _departmentData.Department = _departmentRepository.GetDepartmentById(data.Department.Id) ?? new Department { Name = "Do not choose any department"};
            _departmentData.SubDepartment = _departmentRepository.GetSubDepartmentById(data.SubDepartment.Id) ?? new SubDepartment { Name = "Do not choose any sub-department" };
            _departmentData.DropDown1 = SetDropDown1();
            _departmentData.DropDown2 = SetDropDown2(data.Department.Id);
            return View(_departmentData);
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
            List<Department> departments = _departmentRepository.GetDepartmentLst();
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
                List<SubDepartment> subDepartments = _departmentRepository.GetSubDepartmentList().Where(x => x.ParentId == id).ToList();
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