namespace OfficeAndParkingAPI.DTOs
{
    public class CreateEmployeeDTO(string firstName, string lastName, int teamId)
    {
        public string FirstName { get; init; } = firstName;
        public string LastName { get; init; } = lastName;
        public int TeamId { get; init; } = teamId;
    }
}
