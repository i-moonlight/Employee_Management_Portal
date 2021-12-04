using WebAPI.UserCases.Requests.Employees.Commands.CreateEmployee;

namespace WebAPI.UserCases.Common.Behaviors
{
    public class Validation
    {
        /// <summary>
        /// The instance for employee create command validator.
        /// </summary>
        public static CreateEmployeeCommandValidator CreateEmployeeValidator =>
            new CreateEmployeeCommandValidator();
    }
}