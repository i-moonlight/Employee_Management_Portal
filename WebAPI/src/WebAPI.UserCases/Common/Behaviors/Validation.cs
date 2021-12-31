using WebAPI.UserCases.Requests.Departments.Commands.CreateDepartment;
using WebAPI.UserCases.Requests.Employees.Commands.CreateEmployee;
using WebAPI.UserCases.Requests.Employees.Commands.UpdateEmployee;

namespace WebAPI.UserCases.Common.Behaviors
{
    public class Validation
    {
        /// <summary>
        /// The instance for employee create command validator.
        /// </summary>
        public static CreateEmployeeCommandValidator CreateEmployeeValidator =>
            new CreateEmployeeCommandValidator();

        /// <summary>
        /// The instance for employee update command validator.
        /// </summary>
        public static UpdateEmployeeCommandValidator UpdateEmployeeValidator =>
            new UpdateEmployeeCommandValidator();
        
        /// <summary>
        /// The instance for department create command validator.
        /// </summary>
        public static CreateDepartmentCommandValidator CreateDepartmentValidator =>
            new CreateDepartmentCommandValidator();
    }
}