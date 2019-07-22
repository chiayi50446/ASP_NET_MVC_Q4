using MVC_Q4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Q4.Models.Interface
{
    interface IDepartmentRepository
    {
        List<Department> GetDepartmentLst();

        List<SubDepartment> GetSubDepartmentList();
        Department GetDepartmentById(string Id);

        SubDepartment GetSubDepartmentById(string Id);
    }
}
