using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Lib
{
    public interface IRequestLogger
    {
        void Log(PutMessageRequestType request);
    }
}