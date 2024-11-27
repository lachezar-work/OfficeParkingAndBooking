namespace OfficeAndParking.Services.DTOs.EmployeeDTOs
{
    public class GetEmployeeDTO(string id, string firstName, string lastName, string teamName)
    {
        public string Id { get; init; } = id;
        public string FullName { get; init; } = firstName + ' ' + lastName;
        public string TeamName { get; init; } = teamName;
    }
}
