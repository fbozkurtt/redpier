using Redpier.Domain.Common;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
