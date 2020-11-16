using SquaresGame.Model;
using System;
using System.Threading.Tasks;

namespace SquaresGame
{
    public interface ISquaresGameDataAccess
    {
        Task SaveGameAsync(GameStateWrapper state, String path);
        Task<GameStateWrapper> LoadGameAsync(String path);
    }
}
