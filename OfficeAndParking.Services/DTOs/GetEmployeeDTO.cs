namespace OfficeAndParkingAPI.Services.DTOs
{
    public class GetEmployeeDTO(string id,string firstName, string lastName, string teamName)
    {
        public string Id { get; init; } = id;
        public string FirstName { get; init; } = firstName;
        public string LastName { get; init; } = lastName;
        public string TeamName { get; init; } = teamName;
    }
}
