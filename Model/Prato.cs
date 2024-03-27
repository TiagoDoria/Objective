using System.ComponentModel.DataAnnotations;

namespace DesafioObjective.Model
{
    public class Prato
    {
        public int _Id { get; set; }
        public string _Nome { get; set; }
        public string _Adjetivo { get; set; }
        /* true - Massa
           false - Não é Massa 
         */
        public bool _Massa { get; set; }

        public Prato(int Id,  string Nome, string Adjetivo, bool Massa)
        {
            _Id = Id;
            _Nome = Nome;
            _Adjetivo = Adjetivo;
            _Massa = Massa;
        }

    }
}
