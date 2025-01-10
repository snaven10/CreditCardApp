using MediatR;

public class DeleteCardCommand : IRequest<bool>
{
    public int Id { get; set; }
}
