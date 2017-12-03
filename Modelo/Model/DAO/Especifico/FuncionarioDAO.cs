using Model.Entity;
using System;
using Model.DAO.Generico;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Model.DAO.Especifico
{
	public class FuncionarioDAO
	{
        #region Observa��es

        //Por padr�o, todas as buscas ser�o WHERE STS_ATIVO = 1, exceto a verifica��o se j� existe o cadastro.
        //O prof Cassiano orientou a implementar o setarObjeto() dessa forma que foi feita, pq todas as classes precisam, com parametros e objetos diferentes. N�o vale o trampo de abstrair.
        //O ID da PESSOA ser� trazido na classe controller pra cadastrar aqui.

        #endregion

        #region Objetos
        List<Funcionario> lstFuncionario = new List<Funcionario>();
        dbBancos banco = new dbBancos();
        string query = null;

        #endregion

        #region CRUD

		public bool cadastra(Funcionario funcionario)
		{
            query = null;
            try
            {
                query = "INSERT INTO FUNCIONARIO (ID_CARGO, ID_PESSOA, STS_ATIVO) VALUES ("
                        + (funcionario.cargo).ToString() + ", " 
                        + (funcionario.id_pessoa).ToString() 
                        + ", 1)";
                return true;
            }

            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public List<Funcionario> buscaPorNome(string nome)
        {
            query = null;
            List<Funcionario> lstFuncionarios = new List<Funcionario>();
            try
            {
                query = "SELECT P.NOME, C.DESCRICAO FROM PESSOA AS P " +
                        "INNER JOIN FUNCIONARIO AS F ON P.ID_PESSOA = F.ID_PESSOA " +
                        "INNER JOIN CARGO ON F.ID_CARGO = C.ID_CARGO " +
                        "WHERE P.NOME LIKE '%" + nome + "%' AND F.STS_ATIVO = 1;";
                lstFuncionarios = setarObjeto(banco.MetodoSelect(query));
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return lstFuncionarios;
        }

        public List<Funcionario> buscaPorCargo(string cargo)
        {
            query = null;
            List<Funcionario> lstFuncionarios = new List<Funcionario>();
            try
            {
                query = "SELECT P.NOME, C.DESCRICAO FROM PESSOA AS P " +
                        "INNER JOIN FUNCIONARIO AS F ON P.ID_PESSOA = F.ID_PESSOA " +
                        "INNER JOIN CARGO ON F.ID_CARGO = C.ID_CARGO " +
                        "WHERE C.DESCRICAO LIKE '%" + cargo + "%' AND F.STS_ATIVO = 1;";
                lstFuncionarios = setarObjeto(banco.MetodoSelect(query));
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return lstFuncionarios;
        }

        public List<Funcionario> busca()
        {
            query = null;
            List<Funcionario> lstFuncionarios = new List<Funcionario>();
            try
            {
                query = "SELECT P.NOME, C.DESCRICAO FROM PESSOA AS P " +
                        "INNER JOIN FUNCIONARIO AS F ON P.ID_PESSOA = F.ID_PESSOA " +
                        "INNER JOIN CARGO ON F.ID_CARGO = C.ID_CARGO " +
                        "WHERE F.STS_ATIVO = 1;";
                lstFuncionarios = setarObjeto(banco.MetodoSelect(query));
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return lstFuncionarios;
        }

        public bool altera(Funcionario funcionario) //VERIFICAR
        {
            query = null;
            try
            {
                query = "UPDATE FUNCIONARIO SET ";
                banco.MetodoNaoQuery(query);
                return true;
            }

            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public bool remove(int id)
		{
            query = null;
            try
            {
                query = "UPDATE FUNCIONARIO SET STS_ATIVO = 0 WHERE ID_FUNCIONARIO = " + id.ToString() + ";";
                banco.MetodoNaoQuery(query);
                return true;
            }

            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        
        #endregion

        #region M�todos

        public List<Funcionario> setarObjeto(SqlDataReader dr)
        {
            List<Funcionario> lstFunc = new List<Funcionario>();

            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Funcionario obj = new Funcionario();
                        obj.id_funcionario = Convert.ToInt32(dr["ID_FUNCIONARIO"].ToString());
                        obj.cargo.id_cargo = Convert.ToInt32(dr["ID_CARGO"].ToString());
                        obj.id_pessoa = Convert.ToInt32(dr["ID_PESSOA"].ToString());
                        
                        lstFunc.Add(obj);
                    }
                }
            }

            catch (Exception ex)
            {
                dr.Dispose();
                throw ex;
            }

            return lstFunc;
        }

        #endregion
	}

}

