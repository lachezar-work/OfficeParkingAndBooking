namespace OfficeAndParkingAPI.DTO
{
    public class GetAllEmployeesDTO(string firstName, string lastName, string teamName)
    {
        public string FirstName { get; init; } = firstName;
        public string LastName { get; init; } = lastName;
        public string TeamName { get; init; } = teamName;
    }
}
