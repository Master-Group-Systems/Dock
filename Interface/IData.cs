
namespace Dock.Interface;

public interface IData
{
    void CriarCanco(string conexao);
    void Recuperarbanco();
    void Salvarbanco();
    
    void DropTableAtalho(string conexao);

    string VerificarBanco();
}