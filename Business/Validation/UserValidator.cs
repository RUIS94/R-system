using Model.DTO;

namespace Business.Validation
{
    public static class UserValidator
    {
        public static void ValidateRoleID(int? roleID)
        {
            if (roleID == null || roleID <= 0)
                throw new ArgumentException("Invalid role ID");
        }
        public static void ValidateUser(CreateUserDto dto)
        {
            CommonValidator.ValidateUserName(dto.UserName);
            CommonValidator.ValidateEmail(dto.Email);
            CommonValidator.ValidatePassword(dto.Password);
            CommonValidator.ValidatePhone(dto.Phone);
            ValidateRoleID(dto.RoleID);
        }
        public static void ValidateUser(UpdateUserDto dto)
        {
            CommonValidator.ValidateUserName(dto.UserName);
            CommonValidator.ValidateEmail(dto.Email);
            CommonValidator.ValidatePassword(dto.Password);
            CommonValidator.ValidatePhone(dto.Phone);
            ValidateRoleID(dto.RoleID);
        }
    }
}