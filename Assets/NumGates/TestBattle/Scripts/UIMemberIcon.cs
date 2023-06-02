using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates.TestBattle
{
    public class UIMemberIcon : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;

        private UIAllyManagement uiAllyManagement;
        private int index;
        private bool isAddCharacter = false;

        private void Awake()
        {
            button.onClick.AddListener(OnClickTeamMember);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClickTeamMember);
        }

        public void InitUI(UIAllyManagement uiAllyManagement, int index)
        {
            this.uiAllyManagement = uiAllyManagement;
            this.index = index;
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void EnableAddMember()
        {
            isAddCharacter = true;

            button.GetComponent<Image>().color = Color.green;
        }

        public void DisableAddMember()
        {
            isAddCharacter = false;

            button.GetComponent<Image>().color = Color.white;
        }

        private void OnClickTeamMember()
        {
            if(isAddCharacter == false)
            {
                uiAllyManagement.OnClickTeamMember?.Invoke(index);
            }
            else
            {
                uiAllyManagement.OnClickAddTeamMember?.Invoke(index);
            }
        }
    }
}

