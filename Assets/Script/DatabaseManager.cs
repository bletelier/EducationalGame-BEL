using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using FullSerializer;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        updater = new Updater((int)Random.Range(1, 100000));


    }
    #region UI Elements
    public InputField emailIF;
    public InputField passwordIF;
    public GameObject edadIF;
    public Dropdown carreraDD;
    public Dropdown sexoDD;
    public Dropdown dificultadDD;
    public Dropdown frustracionDD;
    public InputField nombreIF;
    public GameObject rutIF;
    public Text alerta;
    #endregion

    public static LeaderboardUser lUser;
    public static Updater updater ;

    #region URl
    private readonly string AuthKey = "AIzaSyCSnJ5SLQuWxKiYPpwSBFjl7SUMKiYY-Zo";
    private readonly string databaseUrl = "https://test-c3f75.firebaseio.com/";
    private readonly string databaseLeaderboardUrl = "https://proyectobea-bel-leaderboard.firebaseio.com/";
    #endregion

    #region User Data
    public bool first;
    private static string idToken;
    private static string localId;
    public static User user;
    private int edad;
    private string alert;
    #endregion

    #region Surveys
    public void PutSurveyInDatabase(Survey survey)
    {
        ChangeSceneManager.Instance.ChangeScene("CreditosPostSurvey");
        /*RestClient.Put(databaseUrl + "/users/" + localId + "/survey.json?auth=" + idToken, survey).Then(response =>
        {
            DeleteUserAuth();
        });*/
    }

    /*private void DeleteUserAuth()
    {
        string userData = "{\"idToken\":\"" + idToken + "\",\"returnSecureToken\":true}";
        RestClient.Post("https://www.googleapis.com/identitytoolkit/v3/relyingparty/deleteAccount?key=" + AuthKey, userData).Catch(error=>
        {
            Debug.Log(error);
        });
    }*/

    #endregion

    bool ValidarRut(string rut)
    {
        alert = "";
        rut = rut.ToLower();
        if(rut.Length <= 0)
        {
            rutIF.transform.GetChild(1).GetComponent<Text>().color = Color.red;
            rutIF.GetComponent<InputField>().text = "";
            return false;
        }
        if (nombreIF.text.Length <= 0)
        {
            return false;
        }
        if (edadIF.GetComponent<InputField>().text.Length <= 0)
        {
            return false;
        }
        if (carreraDD.value <= 0)
        {
            return false;
        }
        if (sexoDD.value <= 0)
        {
            return false;
        }
        if(rut.Length > 0)
        {
            for(int i = 0; i < rut.Length; ++i)
            {
                if((int)rut[i] < 48 || (int)rut[i] > 57)
                {
                    if (i == rut.Length - 2)
                    {
                        if (rut[i] != '-')
                        {
                            alert = "El rut no es válido\n";
                            rutIF.transform.GetChild(1).GetComponent<Text>().color = Color.red;
                            rutIF.GetComponent<InputField>().text = "";
                            return false;
                        }
                    }
                    else if (i == rut.Length - 1)
                    {
                        if (((int)rut[i] >= 48 && (int)rut[i] <= 57) || rut[i] != 'k')
                        {
                            alert = "El rut no es válido\n";
                            rutIF.transform.GetChild(1).GetComponent<Text>().color = Color.red;
                            rutIF.GetComponent<InputField>().text = "";
                            return false;
                        }
                    }
                    else
                    {
                        alert = "El rut no es válido\n";
                        rutIF.transform.GetChild(1).GetComponent<Text>().color = Color.red;
                        rutIF.GetComponent<InputField>().text = "";
                        return false;
                    }
                }
            }
        }
        if (rut[rut.Length - 2] != '-')
        {
            alert = "El rut no es válido\n";
            rutIF.transform.GetChild(1).GetComponent<Text>().color = Color.red;
            rutIF.GetComponent<InputField>().text = "";
            return false;
        }
        int cont = 0;
        int mult = 2;
        int j = rut.Length - 3;
        while(j >= 0)
        {            
            cont = cont + ((rut[j] - '0') * mult);
            mult++;
            if (mult >= 8)
            {
                mult = 2;
            }
            j--;
        }       
        cont %= 11;
        cont = 11 - cont;
        if(rut[rut.Length - 1] == 'k')
        {
            if(cont == 10)
            {
                return true;
            }
            alert = "El rut no es válido\n";
            rutIF.transform.GetChild(1).GetComponent<Text>().color = Color.red;
            rutIF.GetComponent<InputField>().text = "";
            return false;
        }
        if(cont == 11 && (rut[rut.Length - 1] - '0') == 0)
        {
            return true;
        }
        if(cont == (rut[rut.Length - 1] - '0'))
        {
            return true;
        }
        alert = "El rut no es válido\n";
        rutIF.transform.GetChild(1).GetComponent<Text>().color = Color.red;
        rutIF.GetComponent<InputField>().text = "";
        return false;
    }

    #region Edit User Data in Database
    public void OnEdit()
    {
        if (!ValidarRut(rutIF.transform.GetComponent<InputField>().text))
        {
            if(alert.Length <= 0)
            {
                alert = "Complete todos los campos que se encuentran vacíos";
            }
            alerta.text = alert;
        }
        else
        {
            if (!int.TryParse(edadIF.GetComponent<InputField>().text, out edad))
            {
                edadIF.GetComponent<InputField>().text = "";
                edadIF.transform.GetChild(1).GetComponent<Text>().color = Color.red;
                alert = "Ingrese edad válida";
                alerta.text = alert;
                return;
            }
            
            OnEditButton();
        }
    }

    public void PostDificultadFrustracion()
    {
        
        if(frustracionDD.value > 0 && dificultadDD.value > 0)
        {
            string levelNext = LevelData.levelNext;
            user.niveles[(LevelData.levelName[LevelData.levelName.Length - 1] - '0') - 1].frustracion = frustracionDD.value;
            user.niveles[(LevelData.levelName[LevelData.levelName.Length - 1] - '0') - 1].dificultad = dificultadDD.value;
            PostLevelScoresToDatabase();
            LevelData.CleanStaticsData();
            ChangeSceneManager.Instance.ChangeScene(levelNext);
        }
        else
        {
            Debug.Log("Conteste ambas preguntas");
        }
    }

    private void OnEditButton()
    {
        EditUserThings(rutIF.transform.GetChild(2).GetComponent<Text>().text,nombreIF.text, edad, carreraDD.options[carreraDD.value].text,sexoDD.options[sexoDD.value].text);
    }

    private void EditUserThings(string rut,string nombre, int edad, string carrera, string sexo)
    {
        if (user != null)
        {
            user.rut = rut;
            user.nickName = nombre;
            user.edad = edad;
            user.carrera = carrera;
            user.sexo = sexo;
            lUser = new LeaderboardUser(nombre, 0, carrera);
            
            ChangeSceneManager.Instance.ChangeScene("PantallaDeInicio");
        }
        else
        {
            Debug.Log("Debo Loguear");
            ChangeSceneManager.Instance.ChangeScene("SignIn");
        }

    }

    public void PostLevelScoresToDatabase()
    {
        //updater = new Updater((int)Random.Range(1, 100000));
        //lUser.puntajeAcumulado = user.puntajeAcumulado;
        RestClient.Put(databaseUrl + "users/" + localId + ".json?", user).Then(response=>
        {
            /*RestClient.Put(databaseLeaderboardUrl + "actuals_users/" + localId + ".json", lUser);
            RestClient.Put(databaseLeaderboardUrl + "all_registered_users/" + localId + ".json", lUser);
            RestClient.Put(databaseLeaderboardUrl + "checker/.json", updater);*/
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }
    #endregion

    #region Sign In
    public void OnSignIn()
    {
        SignInButton();
    }

    private void SignInButton()
    {
        localId = CrearIdUsuario();
        user = new User(localId);
        ChangeSceneManager.Instance.ChangeScene("EditUser");
        //SignInUser(emailIF.text, passwordIF.text);
    }

    /*private void SignInUser(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;

                GetUserWhenLogIn();

            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }

    public void GetUserWhenLogIn()
    {        
        RestClient.Get<User>(databaseUrl + "users/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            user = new User();
            user = response;
            ChangeSceneManager.Instance.ChangeScene("EditUser");
        });
    }*/

    string CrearIdUsuario()
    {
        string id = "";
        int character = 0;
        char c;
        for (int i = 0; i < 28; ++i)
        {
            int random = Random.Range(0, 3);
            if(random == 0)
            {
                character = Random.Range(48, 58);
            }
            else if (random == 1)
            {
                character = Random.Range(65, 91);
            }
            else
            {
                character = Random.Range(97, 123);
            }
            c = (char) character;
            id = id + c;
        }
        return id;
    }
    #endregion
}

