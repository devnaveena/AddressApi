using AddressApi.Contracts;
using AddressApi.Entities.DTOs.RequestDto;
using AddressApi.Entities.DTOs.ResponseDto;
using AddressApi.Entities.Helper;
using AddressApi.Repository;

namespace AddressApi.Service
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        /// <summary>
        /// For creating the new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //create
        public string Create(UserDto user)
        {
            if (Usercheck(user))
            {
                string exists = "true";
                return exists;
            }

            User user1 = new User();
            user1.Id = new Guid();
            user1.UserName = user.UserName;
            user1.Password = user.Password;
            user1.FirstName = user.FirstName;
            user1.LastName = user.LastName;
            user1.CreatedOn = DateTime.Now;
            user1.IsActive = true;
            List<Address> addresses = new List<Address>();
            for (int i = 0; i < user.Address.Count; i++)
            {
                Address address1 = new Address()
                {
                    UserId = user1.Id,
                    AddressId = new Guid(),
                    Line1 = user.Address[i].Line1,
                    Line2 = user.Address[i].Line2,
                    City = user.Address[i].City,
                    ZipCode = user.Address[i].ZipCode,
                    State = user.Address[i].stateName,
                    Type = (Guid)Type(user.Address[i].Type.Key),
                    country = (Guid)Type(user.Address[i].country.Key),
                    CreatedOn = DateTime.Now,

                    IsActive = true
                };
                if (address1.Type == null)
                    return "Null";
                addresses.Add(address1);
            }
            user1.Address = addresses;
            List<Email> email = new List<Email>();
            for (int i = 0; i < user.Email.Count; i++)
            {
                if (checkemail(user.Email[i].EmailAddress))
                    return "false";
                Email email1 = new Email()
                {
                    UserId = user1.Id,
                    EmailId = new Guid(),
                    EmailAddress = user.Email[i].EmailAddress,
                    Type = (Guid)Type(user.Email[i].Type.Key),
                    CreatedOn = DateTime.Now,

                    IsActive = true
                };
                if (email1.Type == null)
                    return "Null";
                email.Add(email1);
            }
            user1.Email = email;
            List<PhoneNumber> phone = new List<PhoneNumber>();
            for (int i = 0; i < user.phones.Count; i++)
            {
                PhoneNumber phone1 = new PhoneNumber()
                {
                    UserId = user1.Id,
                    phoneId = new Guid(),
                    PhoneNo = user.phones[i].PhoneNumber,
                    Type = (Guid)Type(user.phones[i].Type.Key),
                    CreatedOn = DateTime.Now,

                    IsActive = true
                };
                if (phone1.Type == null)
                    return "Null";
                phone.Add(phone1);
            }
            user1.PhoneNumber = phone;
            user1.IsActive = true;

            Guid id = _accountRepository.Create(user1);
            if (id == null)
                return "Null";
            _accountRepository.Save();
            return id.ToString();
        }
        /// <summary>
        /// to check the user exist or not
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Usercheck(UserDto user)
        {
            List<User> check = _accountRepository.GetUsers();
            if (check.Count() == 0)
                return false;
            User exist = check.FirstOrDefault(o => o.UserName.Equals(user.UserName));
            if (exist != null)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// return guid
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        //convert string to guid

        public Object Type(string type)
        {
            string s = type.ToUpper();
            RefSet guid = _accountRepository.Matches(s);
            if (guid == null)
                throw new KeyNotFoundException();

            Guid guid1 = guid.RefSetId;

            return guid1;
        }
        public bool checkemail(string email)
        {
            List<Email> list = _accountRepository.GetEmail();
            var result = list.FirstOrDefault(o => o.EmailAddress == email);
            if (result == null)
                return false;
            return true;
        }
        /// <summary>
        /// for getting all the users from the database
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        //get all the users
        public IEnumerable<UserForCreatingDto> GetAll(Pagination pagination)
        {

            List<UserForCreatingDto> userDTOs = new List<UserForCreatingDto>();

            List<User> user = _accountRepository.GetUsers(pagination);
            for (int i = 0; i < user.Count; i++)
            {
                UserForCreatingDto user2 = new UserForCreatingDto();
                if (user[i].IsActive != true)
                    continue;
                user2.Id = user[i].Id;
                user2.FirstName = user[i].FirstName;
                user2.LastName = user[i].LastName;
                user2.Address = new List<AddressForCreatingDto>();
                user2.Email = new List<EmailForCreatingDto>();
                user2.Phone = new List<PhoneForCreatingDto>();
                List<Address> address = _accountRepository.GetAddress(user[i].Id);
                for (int j = 0; j < address.Count; j++)
                {
                    AddressForCreatingDto address2 = new AddressForCreatingDto()
                    {
                        AddressId = address[j].AddressId,
                        Line1 = address[j].Line1,
                        Line2 = address[j].Line2,
                        stateName = address[j].State,
                        ZipCode = address[j].ZipCode,
                        City = address[j].City,
                        country = Key(address[j].country),
                        Type = Key(address[j].Type)
                    };
                    user2.Address.Add(address2);
                }
                List<Email> emails = _accountRepository.GetEmail(user[i].Id);
                for (int j = 0; j < emails.Count; j++)
                {
                    EmailForCreatingDto emailDTO = new EmailForCreatingDto()
                    {
                        EmailId = emails[j].EmailId,
                        EmailAddress = emails[j].EmailAddress,
                        Type = Key(emails[j].Type)
                    };
                    user2.Email.Add(emailDTO);
                }
                List<PhoneNumber> phoneNumbers = _accountRepository.GetPhoneNumber(user[i].Id);
                for (int j = 0; j < phoneNumbers.Count; j++)
                {
                    PhoneForCreatingDto phoneNumberDto = new PhoneForCreatingDto()
                    {
                        PhoneNumberId = phoneNumbers[j].phoneId,
                        PhoneNumber = phoneNumbers[j].PhoneNo,
                        Type = Key(phoneNumbers[j].Type)
                    };
                    user2.Phone.Add(phoneNumberDto);
                }
                userDTOs.Add(user2);
            }
            return userDTOs;
        }
        /// <summary>
        /// returns the type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //convert guid to string
        public TypesDto Key(Guid id)
        {
            RefSet type = _accountRepository.Key(id);
            if (type == null)
                return null;

            string id1 = type.Name;
            TypesDto typesDto = new TypesDto()
            {
                Key = id1
            };

            return typesDto;
        }
        /// <summary>
        /// returns count
        /// </summary>
        /// <returns></returns>
        //Count
        public CountDto GetCount()
        {


            return new CountDto { Count = _accountRepository.CountUsers() }; ;
        }
        /// <summary>
        /// getting user details by using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // get by id
        public UserForCreatingDto GetUserbyId(Guid id)
        {
            UserForCreatingDto user = new UserForCreatingDto();
            User find = (User)_accountRepository.Findbyid(id);
            if (find == null)
                return null;
            user.Id = id;
            user.FirstName = find.FirstName;
            user.LastName = find.LastName;
            user.Address = new List<AddressForCreatingDto>();
            user.Email = new List<EmailForCreatingDto>();
            user.Phone = new List<PhoneForCreatingDto>();
            List<Address> addresses = _accountRepository.GetAddress(id);

            for (int i = 0; i < addresses.Count; i++)
            {
                AddressForCreatingDto address1 = new AddressForCreatingDto()
                {
                    AddressId = addresses[i].AddressId,
                    Line1 = addresses[i].Line1,
                    Line2 = addresses[i].Line2,
                    City = addresses[i].City,
                    ZipCode = addresses[i].ZipCode,
                    stateName = addresses[i].State,
                    Type = Key(addresses[i].Type),
                    country = Key(addresses[i].country)
                };
                user.Address.Add(address1);

            }
            List<Email> email = _accountRepository.GetEmail(id);
            for (int i = 0; i < email.Count; i++)
            {
                EmailForCreatingDto email1 = new EmailForCreatingDto()
                {
                    EmailId = email[i].EmailId,
                    EmailAddress = email[i].EmailAddress,
                    Type = Key(email[i].Type)
                };
                user.Email.Add(email1);
            }
            List<PhoneNumber> phoneNumbers = _accountRepository.GetPhoneNumber(id);
            for (int i = 0; i < phoneNumbers.Count; i++)
            {
                PhoneForCreatingDto phoneNumber1 = new PhoneForCreatingDto()
                {
                    PhoneNumberId = phoneNumbers[i].phoneId,
                    PhoneNumber = phoneNumbers[i].PhoneNo,
                    Type = Key(phoneNumbers[i].Type)
                };
                user.Phone.Add(phoneNumber1);
            }
            if ((bool)find.IsActive)
                return user;
            return null;

        }
        /// <summary>
        /// Update user details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        //update
        public Object Update(Guid id, UserDto userupdate)
        {
            User update = (User)_accountRepository.Findbyid(id);


            if (update == null)
            {
                return null;
            }

            update.UserName = userupdate.UserName;
            update.Password = userupdate.Password;
            update.FirstName = userupdate.FirstName;
            update.LastName = userupdate.LastName;
            update.Address = new List<Address>();
            update.Email = new List<Email>();
            update.PhoneNumber = new List<PhoneNumber>();
            List<Address> addresses = new List<Address>();
            for (int i = 0; i < userupdate.Address.Count; i++)
            {
                Address address1 = new Address()
                {

                    Line1 = userupdate.Address[i].Line1,
                    Line2 = userupdate.Address[i].Line2,
                    City = userupdate.Address[i].City,
                    ZipCode = userupdate.Address[i].ZipCode,
                    State = userupdate.Address[i].stateName,
                    Type = (Guid)Type(userupdate.Address[i].Type.Key),
                    country = (Guid)Type(userupdate.Address[i].country.Key),
                    IsActive = true
                };
                if (address1.Type == null)
                    throw new KeyNotFoundException();
                addresses.Add(address1);

            }
            update.Address = addresses;
            List<Email> email = new List<Email>();
            for (int i = 0; i < userupdate.Email.Count; i++)
            {
                if (checkemail(userupdate.Email[i].EmailAddress))
                    throw new KeyNotFoundException();
                Email email1 = new Email()
                {

                    EmailAddress = userupdate.Email[i].EmailAddress,
                    Type = (Guid)Type(userupdate.Email[i].Type.Key),
                    IsActive = true
                };
                if (email1.Type == null)
                    throw new KeyNotFoundException();
                email.Add(email1);
            }

            update.Email = email;
            List<PhoneNumber> phone = new List<PhoneNumber>();
            for (int i = 0; i < userupdate.phones.Count; i++)
            {
                PhoneNumber phone1 = new PhoneNumber()
                {

                    PhoneNo = userupdate.phones[i].PhoneNumber,
                    Type = (Guid)Type(userupdate.phones[i].Type.Key),
                    IsActive = true
                };
                if (phone1.Type == null)
                    throw new KeyNotFoundException();
                phone.Add(phone1);
            }
            update.PhoneNumber = phone;
            _accountRepository.Update(update);
            _accountRepository.Save();
            UpdateDto user = new UpdateDto();
            user.Id = update.Id;
            user.UserName = update.UserName;
            user.FirstName = update.FirstName;
            user.LastName = update.LastName;
            user.Password = update.Password;
            user.Address = new List<AddressDto>();
            user.Email = new List<EmailDto>();
            user.phoneNumber = new List<PhoneNumberDto>();
            List<Address> addresseslist = _accountRepository.GetAddress(id);

            for (int i = 0; i < addresses.Count; i++)
            {
                AddressDto address1 = new AddressDto()
                {

                    Line1 = addresseslist[i].Line1,
                    Line2 = addresseslist[i].Line2,
                    City = addresseslist[i].City,
                    ZipCode = addresseslist[i].ZipCode,
                    stateName = addresseslist[i].State,
                    Type = Key(addresseslist[i].Type),
                    country = Key(addresseslist[i].country)
                };
                user.Address.Add(address1);

            }
            List<Email> emailList = _accountRepository.GetEmail(id);
            for (int i = 0; i < email.Count; i++)
            {

                EmailDto email1 = new EmailDto()
                {
                    EmailAddress = emailList[i].EmailAddress,
                    Type = Key(emailList[i].Type)
                };
                user.Email.Add(email1);
            }
            List<PhoneNumber> phoneNumbers = _accountRepository.GetPhoneNumber(id);
            for (int i = 0; i < phoneNumbers.Count; i++)
            {
                PhoneNumberDto phoneNumber1 = new PhoneNumberDto()
                {
                    PhoneNumber = phoneNumbers[i].PhoneNo,
                    Type = Key(phoneNumbers[i].Type)
                };
                user.phoneNumber.Add(phoneNumber1);
            }

            return user;
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //delete
        public Object Delete(Guid id)
        {
            bool delete = _accountRepository.Delete(id);


            _accountRepository.Save();
            string result = "User deleted";
            if (delete)
            {

                return result;
            }
            return null;

        }
        /// <summary>
        /// Stores the byte[] form of a file in the database
        /// </summary>
        /// <param name="content"></param>
        /// <param name="userId"></param>

        public UploadFileDto UploadFile(FileDto content, Guid userId)
        {

            if (content == null)
                throw new Exception();

            IFormFile file = content.file;
            FileModel fileModel = new FileModel();
            fileModel.Id = new Guid();
            fileModel.UserId = userId;
            fileModel.FileType = content.file.ContentType;
            byte[] GetBytes(IFormFile File)
            {
                using MemoryStream memoryStream = new MemoryStream();
                File.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
            byte[] bytes = GetBytes(file);

            fileModel.file = bytes;

            _accountRepository.UploadFile(fileModel);
            _accountRepository.Save();
            UploadFileDto fileDto = new UploadFileDto()
            {
                Id = fileModel.Id,
                UserId = userId,
                FileName = "Application",
                DownloadUrl = "https://localhost:44350/api/asset?assetId=" + fileModel.Id,
                FileType = fileModel.FileType,


            };
            return fileDto;
        }
        /// <summary>
        /// returns a stored file from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Tuple<FileModel, string> DownloadFile(Guid id)
        {

            var res = _accountRepository.GetFile(id);
            if (res == null)
                return null;
            return res;
        }
        /// <summary>
        /// Check the user in the database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user" ></param>
        /// <returns></returns>
        public bool UserExist(Guid userId,UserDto user)
        {

            if (_accountRepository.UserExist(userId, user)) return true;
            

            return false;
        }
        public bool CheckAssetId(Guid assetId, Guid userId)
        {
            return _accountRepository.CheckAssetId(assetId, userId);
        }
        public MetadataDto Metadata(string key)
        {
            var metadata = new MetadataDto();
            metadata.Id = _accountRepository.Metadata(key);
            metadata.key = key;

            return metadata;
        }
    }
}