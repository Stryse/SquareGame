using SquaresGame.Model;
using System;
using System.Threading.Tasks;

namespace SquaresGame
{
    public interface ISquaresGameDataAccess
    {
        Task SaveGameAsync(GameStateWrapper state);
        Task<GameStateWrapper> LoadGameAsync(String path);
    }
}
