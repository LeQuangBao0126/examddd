using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Examination.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Examination.Infrastructure.SeedWork;

//Adapter of mongoDB
public class BaseRepository<T> : IRepositoryBase<T> where T : Entity , IAggregateRoot
{
    private readonly IMongoClient _mongoClient;
    private readonly IClientSessionHandle _clientSessionHandle;
    private readonly string _collection ;
    private readonly ExamSettings _settings ;
    private readonly IMediator _mediator;

    public BaseRepository(IMongoClient mongoClient , IClientSessionHandle clientSessionHandle ,
        IOptions<ExamSettings> setting , IMediator mediator , string collection )
    {
        _settings = setting.Value;
        (_mongoClient , _clientSessionHandle , _collection) = (mongoClient,clientSessionHandle,collection);
        _mediator = mediator;
        if (!_mongoClient.GetDatabase(_settings.DatabaseSettings.DatabaseName).ListCollectionNames().ToList().Contains(collection))
            _mongoClient.GetDatabase(_settings.DatabaseSettings.DatabaseName).CreateCollection(collection);
    }

    protected  virtual IMongoCollection<T> Collection =>
          _mongoClient.GetDatabase(_settings.DatabaseSettings.DatabaseName).GetCollection<T>(_collection);
    
    
    public async Task InsertAsync(T obj)
    {
        await Collection.InsertOneAsync(_clientSessionHandle , obj);
    }

    public async Task UpdateAsync(T obj)
    {
        await Collection.ReplaceOneAsync(x => x.Id == obj.Id, obj);
       // throw new Exception();
    }

    public Task DeleteAsync(string id)
    {
        throw new System.NotImplementedException();
    }

    public void StartTransaction()
    {
        _clientSessionHandle.StartTransaction();
    }
    public async  Task  AbortTransactionAsync(CancellationToken cancellationToken = default)
    {
       await   _clientSessionHandle.AbortTransactionAsync(cancellationToken);
    }
    public async Task CommitTransactionAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _clientSessionHandle.CommitTransactionAsync(cancellationToken);
        //publish event when entity has been commit to database 
        var domainEvents = entity.DomainEvents.ToList();
        foreach (var domainEvent in domainEvents )
        {
           await _mediator.Publish(domainEvent);
        } 
    }
}