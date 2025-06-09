namespace src.DeviceEmployeeAuthManager.DTO;

public class PersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string PassportNumber { get; set; }
    public string Email { get; set; }

    public PersonDto()
    {
    }

    public PersonDto(int id, string firstName, string? middleName, string lastName, string phoneNumber, string passportNumber, string email)
    {
        Id = id;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        PassportNumber = passportNumber;
        Email = email;
    }
}