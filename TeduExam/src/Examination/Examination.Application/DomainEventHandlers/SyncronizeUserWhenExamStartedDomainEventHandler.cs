using System.Threading;
using System.Threading.Tasks;
using Examination.Domain.AggregateModels.UserAggregate;
using Examination.Domain.Events;
using MediatR;

namespace Examination.Application.DomainEventHandlers;

public class SyncronizeUserWhenExamStartedDomainEventHandler  :INotificationHandler<ExamStartedDomainEvent>
{
    private IUserRepository _userRepository;

    public SyncronizeUserWhenExamStartedDomainEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(ExamStartedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(notification.UserId);

        if (user == null)
        {
            _userRepository.StartTransaction();
            user = User.CreateNewUser(notification.UserId, notification.FirstName, notification.LastName);
            await _userRepository.InsertAsync(user);
            await _userRepository.CommitTransactionAsync(user , cancellationToken);
        }
        else
        {
            
        }
        // cái này seẽ nhiều sự kien bao gồm gửi mail tới user , bắn notifiaction qua RBMQ hoặc qua socket IO , hoặc là 1 hoạt động gọi qua rest APU lấy data
        
    }
}