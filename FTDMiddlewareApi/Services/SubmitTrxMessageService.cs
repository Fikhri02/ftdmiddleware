using FTDMiddlewareApi.Service.Interface;
using FTDMiddlewareDataAccess.Models.RequestModels;
using FTDMiddlewareDataAccess.Models.ResponseModels;

namespace FTDMiddlewareApi.Service;

class SubmitTrxMessageService : ISubmitTrxMessageService
{
    public SubmitTrxMessageResponse SubmitTrxMessage(SubmitTrxMessageRequest request)
    {
        SubmitTrxMessageResponse response = new SubmitTrxMessageResponse();

        long totalAmount = request.Items.Sum(item => item.UnitPrice * item.Qty);
        double mandatoryDiscount = 0;

        switch(request.TotalAmount){
            case < 200: mandatoryDiscount = 0; break;
            case >= 200 and <= 500: mandatoryDiscount = 0.05; break;
            case >= 501 and <= 800: mandatoryDiscount = 0.07; break;
            case >= 801 and <= 1200: mandatoryDiscount = 0.10; break;
            case > 1200: mandatoryDiscount = 0.15; break;
        }

        double conditionalDiscount = 0;

        if (request.TotalAmount > 500 && request.TotalAmount % 2 == 1)
        {
            conditionalDiscount += 0.08;
        }

        if (request.TotalAmount > 900 && request.TotalAmount % 10 == 5)
        {
            conditionalDiscount += 0.10;
        }

        double maxDiscount = Math.Min(mandatoryDiscount + conditionalDiscount, 0.2);

        long totalDiscount = (long)(totalAmount * maxDiscount);

        if (request.Items.Count > 0 && totalAmount != request.TotalAmount)
        {
            response.Result = 0;
            response.ResultMessage = "Invalid Total Amount.";
            return response;
        }

        response.Result = 1;
        response.TotalAmount = totalAmount;
        response.TotalDiscount = totalDiscount;
        response.FinalAmount = totalAmount - totalDiscount;

        return response;
    }
}