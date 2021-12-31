using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Infrastructure.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Examination.Infrastructure.Repositories;

public class QuestionRepository : BaseRepository<Question> , IQuestionRepository
{
    public QuestionRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, IOptions<ExamSettings> setting, IMediator mediator) :
        base(mongoClient, clientSessionHandle, setting, mediator, Constants.Collections.Question)
    {
    }
    
    
}