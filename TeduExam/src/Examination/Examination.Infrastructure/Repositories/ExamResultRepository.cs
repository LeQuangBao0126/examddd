using System.Threading.Tasks;
using Examination.Domain.AggregateModels.ExamResultAggregate;
using Examination.Infrastructure.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Examination.Infrastructure.Repositories;

public class ExamResultRepository: BaseRepository<ExamResult> , IExamResultRepository 
{
    public ExamResultRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, IOptions<ExamSettings> setting, IMediator mediator) : 
        base(mongoClient, clientSessionHandle, setting, mediator, Constants.Collections.ExamResult)
    {
    }

    public Task<ExamResult> GetDetails(string userId, string examId)
    {
        throw new System.NotImplementedException();
    }
}