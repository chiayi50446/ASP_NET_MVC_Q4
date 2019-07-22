using MVC_Q4.Models.Interface;
using MVC_Q4.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Q4.Models.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public List<Department> GetDepartmentLst()
        {
            String PATH = String.Format(@"{0}\Content\department.json", System.AppDomain.CurrentDomain.BaseDirectory);
            string json = System.IO.File.ReadAllText(PATH);
            string str = json.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            return JsonConvert.DeserializeObject<List<Department>>(json);
        }

        public List<SubDepartment> GetSubDepartmentList()
        {
            String PATH = String.Format(@"{0}\Content\sub_department.json", System.AppDomain.CurrentDomain.BaseDirectory);
            string json = System.IO.File.ReadAllText(PATH);
            string str = json.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            return JsonConvert.DeserializeObject<List<SubDepartment>>(json);
        }

        public Department GetDepartmentById(string Id)
        {
            List<Department> dataList = GetDepartmentLst();
            Department result = dataList.FirstOrDefault(x => x.Id == Id);
            return result;
        }

        public SubDepartment GetSubDepartmentById(string Id)
        {
            List<SubDepartment> dataList = GetSubDepartmentList();
            SubDepartment result = dataList.FirstOrDefault(x => x.Id == Id);
            return result;
        }
    }
}