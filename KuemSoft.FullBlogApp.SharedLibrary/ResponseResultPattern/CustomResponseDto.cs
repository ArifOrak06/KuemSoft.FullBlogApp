using KuemSoft.FullBlogApp.SharedLibrary.Enums;

namespace KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern
{
    public  class CustomResponseDto<T> where T : class,new()
    {
        public T Data { get; set; }
        public List<CustomValidationError> ValidationErrors { get; set; }
        public List<string> Errors { get; set; }
        public ResponseType ResponseType { get; set; }
        public string IsSuccessMessage { get; set; }
        public List<CustomIdentityError> IdentityErrors { get; set; }

        public static CustomResponseDto<NoContentDto> Success(ResponseType responseType, string isSuccessMessage)
        {
            return new CustomResponseDto<NoContentDto> { ResponseType = responseType, IsSuccessMessage = isSuccessMessage };
        }
        public static CustomResponseDto<T> Success(ResponseType responseType,T data, string isSuccessMessage)
        {
            return new CustomResponseDto<T> { ResponseType = responseType, Data = data, IsSuccessMessage = isSuccessMessage };
        }
        public static CustomResponseDto<T> Fail(ResponseType responseType, List<string> errorMessages)
        {
            return new CustomResponseDto<T> { ResponseType = responseType, Errors = errorMessages };
        }
        public static CustomResponseDto<T> Fail(ResponseType responseType, string errorMessage)
        {
            return new CustomResponseDto<T> { ResponseType = responseType, Errors = new() { errorMessage } };
        }

        public static CustomResponseDto<T> ValidationFail(ResponseType responseType, List<CustomValidationError> validationErrors)
        {
            return new CustomResponseDto<T> { ResponseType = responseType, ValidationErrors = validationErrors };
        }
        public static CustomResponseDto<T> ValidationFail(ResponseType responseType, CustomValidationError validationError)
        {
            return new CustomResponseDto<T> { ResponseType = responseType, ValidationErrors = new() { validationError } };
        }
        public static CustomResponseDto<T> IdentityFail(ResponseType responseType, List<CustomIdentityError> ıdentityErrors)
        {
            return new CustomResponseDto<T> { ResponseType = responseType, IdentityErrors = ıdentityErrors };
        }
        public static CustomResponseDto<T> IdentityFail(ResponseType responseType, CustomIdentityError identityError)
        {
            return new CustomResponseDto<T> { ResponseType = responseType, IdentityErrors = new() { identityError } };
        }
        public static CustomResponseDto<T> ValidUpdateFail(ResponseType responseType, T data, List<CustomValidationError> validationErrors)
        {
            return new CustomResponseDto<T>
            {
                ResponseType = responseType,
                Data = data,
                ValidationErrors = validationErrors
            };
        }
        public static CustomResponseDto<T> ValidUpdateFail(ResponseType responseType, T data, CustomValidationError validationError)
        {
            return new CustomResponseDto<T>
            {
                ResponseType = responseType,
                Data = data,
                ValidationErrors = new() { validationError }
            };

        }
    }
}
