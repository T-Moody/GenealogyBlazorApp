# Blazor Component Development Instructions

## Component Architecture Guidelines

### Component Structure

When creating or modifying Blazor components in the `/Components/` directory:

1. **Single Responsibility**: Each component should have one clear purpose
2. **Parameter Validation**: Always validate component parameters
3. **State Management**: Use cascading values for shared state, component state for local data
4. **Event Handling**: Prefer async event handlers and proper error handling

### Code Patterns to Follow

#### Component Parameters

```csharp
[Parameter] public string? Title { get; set; }
[Parameter] public EventCallback<ModelType> OnItemSelected { get; set; }
[Parameter] public RenderFragment? ChildContent { get; set; }

protected override void OnParametersSet()
{
    // Validate parameters here
    if (string.IsNullOrEmpty(RequiredParameter))
        throw new ArgumentException("Required parameter cannot be null");
}
```

#### Event Handling

```csharp
private async Task HandleClickAsync()
{
    try
    {
        StateHasChanged(); // Update UI immediately if needed
        await OnItemSelected.InvokeAsync(selectedItem);
        await NotifyHub(); // SignalR notifications where appropriate
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error handling click event");
        // Show user-friendly error message
    }
}
```

#### SignalR Integration

Components that need real-time updates should:

```csharp
@implements IAsyncDisposable
@inject IJSRuntime JSRuntime

protected override async Task OnInitializedAsync()
{
    await hubConnection.StartAsync();
    hubConnection.On<UpdateMessage>("UpdateReceived", HandleUpdate);
}

public async ValueTask DisposeAsync()
{
    if (hubConnection is not null)
    {
        await hubConnection.DisposeAsync();
    }
}
```

### UI/UX Standards

#### Bootstrap Integration

- Use Bootstrap 5 classes consistently
- Follow responsive design patterns (mobile-first)
- Use component-scoped CSS for custom styling

#### Accessibility Requirements

- Include appropriate ARIA labels and roles
- Ensure keyboard navigation works properly
- Maintain proper heading hierarchy
- Include alt text for images and icons

#### Loading States

```razor
@if (isLoading)
{
    <div class="d-flex justify-content-center">
        <div class="spinner-border" role="status">
            <span class="visually-hidden">Loading...</span>
        </div>
    </div>
}
else
{
    <!-- Component content -->
}
```

### Performance Considerations

1. **Efficient Rendering**: Use `@key` for list items, avoid unnecessary re-renders
2. **Lazy Loading**: Implement for large datasets or complex components
3. **Memory Management**: Properly dispose of resources and event subscriptions
4. **Minimal State Changes**: Only call `StateHasChanged()` when necessary

### Component Testing

Every new component should include:

```csharp
public class ComponentNameTests : TestContext
{
    [Fact]
    public void Component_RendersCorrectly_WithValidParameters()
    {
        // Arrange
        var component = RenderComponent<ComponentName>(parameters => parameters
            .Add(p => p.Title, "Test Title"));

        // Act & Assert
        component.Find("h1").TextContent.Should().Be("Test Title");
    }
}
```

## Specific Component Guidelines

### Ontology Components (`/Components/Ontology/`)

- Always validate concept and relationship data
- Include proper authorization checks
- Implement optimistic UI updates with rollback capability
- Show appropriate loading states during operations

### Layout Components (`/Components/Layout/`)

- Maintain consistent navigation patterns
- Include user presence indicators where appropriate
- Handle responsive design for mobile devices

### Shared Components (`/Components/Shared/`)

- Design for reusability across different contexts
- Include comprehensive parameter validation
- Document usage examples in XML comments
