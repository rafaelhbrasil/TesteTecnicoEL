namespace TesteTecnicoEL.Dominio
{
    public abstract class Entidade : EntidadeValidacao
    {
        public long Id { get; set; }

        public virtual void SetId(long id)
        {
            Id = id;
        }

    }
}
