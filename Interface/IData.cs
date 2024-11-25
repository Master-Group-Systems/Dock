
namespace Dock.Interface;

public interface IData
{
    void CriarBanco(string conexao);
    
    void DropTableAtalho(string conexao);

    string VerificarBanco();
}