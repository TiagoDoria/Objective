using DesafioObjective.Model;
using Microsoft.VisualBasic;

namespace DesafioObjective
{
    public class JogoGourmet
    {
        private List<Prato> PratosLista;
        private int id = 0;
        string nomePrato;
        public JogoGourmet()
        {
            PratosLista = new List<Prato>();
            // true - Massa /false - Não é Massa
            PratosLista.Add(new Prato(id++, "Lasanha", "", true));
            PratosLista.Add(new Prato(id++, "Bolo de Chocolate", "", false));
        }  

        public void Iniciar()
        {
            MessageBox.Show("Pense em um prato que gosta", "Jogo Gourmet", MessageBoxButtons.OK);
        }

        public DialogResult MensagemPergunta(string mensagem)
        {
            return MessageBox.Show(mensagem,"Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void Acerto()
        {
            MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Jogar();
        }

        public DialogResult AdivinharPrato(List<Prato> pratoLista)
        {
            restart:
            foreach (var prato in pratoLista.OrderByDescending(x => x._Id))
            {
                if (prato._Nome.Equals("Lasanha") || prato._Nome.Equals("Bolo de Chocolate"))
                {
                    nomePrato = prato._Nome;
                    return MensagemPergunta($"O prato que você pensou é { prato._Nome }?");
                }

                if (MensagemPergunta($"O prato que você pensou é { prato._Adjetivo }?") == DialogResult.Yes)
                {
                    nomePrato = prato._Nome;
                    pratoLista = pratoLista.Where(x => x._Adjetivo == prato._Adjetivo && !pratoLista.Contains(prato)).ToList();
                    if(MensagemPergunta($"O prato que você pensou é { prato._Nome} ?") == DialogResult.Yes)
                    {
                        return DialogResult.Yes;
                    }
                    goto restart;
                }           
            }

            return DialogResult.No;
        }

        public void CadastrarPrato(bool massa)
        {
            string nome = Interaction.InputBox("Qual prato você pensou?", "Desisto");
            string adjetivo = Interaction.InputBox($"{ nome } é ________ mas { nomePrato } não", "Complete");
            PratosLista.Add(new Prato(id++, nome, adjetivo, massa));
        }

        public void Jogar()
        {
            bool massa;
            DialogResult respostaPrato;
            List<Prato> listaPratos = PratosLista;

            Iniciar();

            DialogResult respostaMassa = MensagemPergunta("O prato que você pensou é massa?");
            
            if (respostaMassa == DialogResult.Yes)
            {
                listaPratos = PratosLista.Where(x => x._Massa == true).ToList();
                massa = true;
            }
            else
            {
                listaPratos = PratosLista.Where(x => x._Massa == false).ToList();
                massa = false;
            }

            respostaPrato = AdivinharPrato(listaPratos);

            if (respostaPrato == DialogResult.Yes) Acerto();
            else 
            {
                CadastrarPrato(massa);
                Jogar();
            }
        }
    }
}
