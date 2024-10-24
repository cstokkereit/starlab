using AutoMapper;

namespace StarLab.Application
{
    public abstract class UseCaseInteractor<T>
    {
        private readonly IMapper mapper;

        private readonly T outputPort;

        public UseCaseInteractor(T outputPort, IMapper mapper)
        {
            this.outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected IMapper Mapper => mapper;

        protected T OutputPort => outputPort;
    }
}
