using MediatR;

namespace Gamba.Application.Configuration.Commands;

public interface ICommandHandler<in TCommand> :
    IRequestHandler<TCommand> where TCommand : ICommand<Unit>
{

}

public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{

}