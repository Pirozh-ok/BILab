namespace BILab.Domain.DTOs.Base
{
    public class ControllerResponseDTO
    {
        public class OkResponseResultDto
        {
            public string Message { get; set; }
        }

        public class OkResponseResultModel<T> : OkResponseResultDto
        {
            public T Data { get; set; }
        }

        public class ErrorResponseResultDto
        {
            public List<string> Error { get; set; }
        }
    }
}
