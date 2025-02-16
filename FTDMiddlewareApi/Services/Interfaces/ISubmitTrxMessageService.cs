using FTDMiddlewareDataAccess.Models.RequestModels;
using FTDMiddlewareDataAccess.Models.ResponseModels;

namespace FTDMiddlewareApi.Service.Interface
{
    public interface ISubmitTrxMessageService
    {
        public SubmitTrxMessageResponse SubmitTrxMessage(SubmitTrxMessageRequest request);
    }
}
