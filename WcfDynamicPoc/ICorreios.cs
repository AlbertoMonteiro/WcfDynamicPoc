using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WcfDynamicPoc
{
    [ServiceContract(Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/")]
    [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Literal)]
    public interface ICorreios
    {
        [OperationContract(Action = "", ReplyAction = "*")]
        [return: MessageParameter(Name = "return")]
        string consultaCEP(string cep);
        //void consultaCEP([XmlElement( Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/")] XmlNode cep);

        //[OperationContract(Action = "", ReplyAction = "*")]
        //[XmlSerializerFormat(SupportFaults = true)]
        //ConsultaCepResponse PegaDados(ConsultaCep cep);
    }

    [MessageContract(WrapperName = "consultaCEP", WrapperNamespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/", IsWrapped = true)]
    public class ConsultaCep
    {
        [MessageBodyMember(Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/", Order = 0)]
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string cep;

        public ConsultaCep(string cep)
        {
            this.cep = cep;
        }
    }

    [MessageContract(WrapperName = "consultaCEPResponse", WrapperNamespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/", IsWrapped = true)]
    public class ConsultaCepResponse
    {
        [MessageBodyMember(Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/", Order = 0)]
        [XmlElement("return", Form = XmlSchemaForm.Unqualified)]
        public Endereco Endereco;
    }

    [Serializable]
    [XmlType(Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/")]
    public class Endereco
    {
        public override string ToString()
        {
            return $"{nameof(bairro)}: {bairro}, {nameof(cep)}: {cep}, {nameof(cidade)}: {cidade}, {nameof(complemento)}: {complemento}, {nameof(complemento2)}: {complemento2}, {nameof(end)}: {end}, {nameof(id)}: {id}, {nameof(uf)}: {uf}";
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string bairro { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public uint cep { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string cidade { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public string complemento { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public string complemento2 { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public string end { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public byte id { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public string uf { get; set; }
    }

}