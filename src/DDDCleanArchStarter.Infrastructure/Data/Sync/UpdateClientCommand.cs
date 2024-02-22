using MediatR;
namespace DDDCleanArchStarter.Infrastructure.Data.Sync
{
    public class UpdateClientCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}