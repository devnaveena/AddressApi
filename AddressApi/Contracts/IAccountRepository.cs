using AddressApi.Entities.DTOs.ResponseDto;
using AddressApi.Entities.Helper;

namespace AddressApi.Contracts
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Get the List of Users
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers();
        /// <summary>
        /// To return guid id of type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Guid</returns>
        public RefSet Matches(string type);
        /// <summary>
        /// get the list of email
        /// </summary>
        /// <returns></returns>
        public List<Email> GetEmail();
        /// <summary>
        /// To create the new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>user id</returns>
        public Guid Create(User user);
        /// <summary>
        /// To Save the changes in the database
        /// </summary>
        public void Save();
        public List<User> GetUsers(Pagination pagination);
        /// <summary>
        /// Get the address of the User by userid
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public List<Address> GetAddress(Guid id);
        /// <summary>
        /// Find the key
        /// </summary>
        /// <param name="id"></param>
        /// <returns>RefSet</returns>
        public RefSet Key(Guid id);
        /// <summary>
        /// Get the Email by userid
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of Emails</returns>
        public List<Email> GetEmail(Guid id);
        /// <summary>
        /// Get the phone number by userid
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of Numbers</returns>
        public List<PhoneNumber> GetPhoneNumber(Guid id);
        /// <summary>
        /// Find the id in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>

        public Object Findbyid(Guid id);
        /// <summary>
        /// to Update user details in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Update(User user);
        /// <summary>
        /// To Delete the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool Delete(Guid id);
        /// <summary>
        /// To upload the file in the database
        /// </summary>
        /// <param name="content"></param>
        /// <returns>IFormFile</returns>
        public void UploadFile(FileModel content);
        // <summary>
        /// to get the file
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Tuple<FileModel, string> GetFile(Guid id);
        public bool UserExist(Guid userId, UserDto user);
        public bool CheckAssetId(Guid assetId, Guid userId);
        public int CountUsers();
        public Guid Metadata(string key);


    }
}
