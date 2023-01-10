using AddressApi.Entities.DTOs.RequestDto;
using AddressApi.Entities.DTOs.ResponseDto;
using AddressApi.Entities.Helper;

namespace AddressApi.Contracts
{
    public interface IAccountService
    {
        /// <summary>
        /// Create a new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string Create(UserDto user);
        /// <summary>
        /// Get all the user from the database
        /// </summary>
        /// <returns></returns>
        public PagedList<UserForCreatingDto> GetAll(Pagination pagination);
        /// <summary>
        /// Get the count of the user in the database
        /// </summary>
        /// <returns></returns>
        public CountDto GetCount();

        /// <summary>
        /// Get the user details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserForCreatingDto GetUserbyId(Guid id);
        /// <summary>
        /// To update user int the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Object Update(Guid id, UserDto user);
        /// <summary>
        /// Delete the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Object Delete(Guid id);
        /// <summary>
        /// To upload the file in the database
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public UploadFileDto UploadFile(FileDto content, Guid userId);
        /// <summary>
        /// returns a stored file from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Tuple<FileModel, string> DownloadFile(Guid id);
        /// <summary>
        /// Check the user in the database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user" ></param>
        /// <returns></returns>
        public bool UserExist(Guid userId, UserDto user);
        public bool CheckAssetId(Guid assetId, Guid userId);
        /// <summary>
        /// Get metadata  from the database
        /// </summary>
        /// <param name="key"></param>
        public MetadataDto Metadata(string key);



    }
}
