namespace Domain.Event;

public record ProductCreatedEvent(
    int ProductId,
    string ProductName);