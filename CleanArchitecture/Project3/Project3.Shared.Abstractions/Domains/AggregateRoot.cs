using Project3.Shared.Abstractions.Domains;

namespace Project3.Shared.Abstractions.Domains
{
    // Đây là một lớp trừu tượng (abstract class) đại diện cho Aggregate Root
    // trong kiến trúc DDD (Domain-Driven Design).
    public abstract class AggregateRoot<T>
    {
        // Thuộc tính Id đại diện cho định danh của Aggregate Root.
        public T Id { get; protected set; }

        // Thuộc tính Version đại diện cho phiên bản của Aggregate Root.
        public int Version { get; protected set; }

        // Biến _versionIncremented được sử dụng để theo dõi việc tăng phiên bản.
        private bool _versionIncremented;

        // Thuộc tính Events trả về danh sách các sự kiện do Aggregate Root tạo ra.
        public IEnumerable<IDomainEvent> Events => _events;

        // Danh sách chứa các sự kiện.
        private readonly List<IDomainEvent> _events = new();

        // Phương thức AddEvent được sử dụng để thêm một sự kiện vào danh sách và tăng phiên bản nếu cần thiết.
        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any() && !_versionIncremented)
            {
                Version++;
                _versionIncremented = true;
            }

            _events.Add(@event);
        }

        // Phương thức ClearEvents được sử dụng để xóa tất cả sự kiện khỏi danh sách.
        public void ClearEvents() => _events.Clear();

        // Phương thức IncrementVersion tăng phiên bản nếu chưa được tăng trước đó.
        protected void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }

            Version++;
            _versionIncremented = true;
        }
    }
}