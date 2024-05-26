
using Domain.Entity;
using System.Text.Json;

namespace Application.Command.Factory;

using System;
using Domain.Interfaces;

public class CommandFactory
{
    public ICommand CreateCommand(string message)
    {
        var command = JsonSerializer.Deserialize<TemplateMessage>(message);

        var type = Type.GetType($"Application.Command.{command.CommandType}Command");
        if (type == null)
        {
            throw new InvalidOperationException($"Invalid command type: {command.CommandType}");
        }

        var dataJson = JsonSerializer.Serialize(command.Data);
        return (ICommand)Activator.CreateInstance(type,  dataJson);
    }
}