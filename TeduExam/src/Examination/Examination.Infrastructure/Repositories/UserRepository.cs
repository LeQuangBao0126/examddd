using System.Threading.Tasks;
using Examination.Domain.AggregateModels.UserAggregate;
using Examination.Infrastructure.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Examination.Infrastructure.Repositories;

public class UserRepository  : BaseRepository<User> , IUserRepository
{
    public UserRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, IOptions<ExamSettings> setting, IMediator mediator ) :
        base(mongoClient, clientSessionHandle, setting, mediator, Constants.Collections.User)
    {
    }

    public async Task<User> GetUserByIdAsync(string externalId)
    {
        var filter = Builders<User>.Filter.Eq(x => x.Id , externalId);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }
}