using Dev.NucleaTNT.Squared.Managers;
using Photon.Pun;

namespace Dev.NucleaTNT.PUNTesting
{
    public class LoadSceneButton : MasterClientButton
    {
        public void OnClick_LoadScene(int buildIndex)
        {
            PhotonNetwork.LoadLevel(buildIndex);
        }
        
        public void OnClick_LoadScene(string sceneName)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}