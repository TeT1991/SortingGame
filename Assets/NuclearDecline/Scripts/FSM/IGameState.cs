using NuclearDecline.Bootstraps;
namespace NuclearDecline.FSM
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}

