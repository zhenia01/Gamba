using MediatR;

namespace Gamba.Application.Configuration.Commands;

public interface ICommand<out TResult> : IRequest<TResult>{ }