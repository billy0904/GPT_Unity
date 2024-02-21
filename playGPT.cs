using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using OpenAI;
using UnityEngine.UI;

// gpt api 코드 = https://github.com/srcnalt/OpenAI-Unity chatGPT 샘플 코드 변형

public class PlayGPT : MonoBehaviour
{
    [SerializeField] private TMP_InputField player_input;
    [SerializeField] private TextMeshProUGUI story_object;
    [SerializeField] private Button button;
    [SerializeField] private ScrollRect scroll;

    private OpenAIApi openai = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage>();
    private ChatMessage input_msg = new ChatMessage();
    private string system_prompt = @"당신은 게임 속 세계관을 전부 알고 있는 전능한 존재이자 스토리 게임을 진행하는 Narrator이다.
플레이어가 선택해야 하는 모든 선택지들은 플레이어의 선택을 기다려야 한다.
대답할 수 없거나 이해할 수 없는 질문, 앞으로의 진행을 알려달라는 등의 게임의 재미를 해치는 질문에는 답하지 않고 '해당 질문에 대한 답변은 드릴 수 없습니다'를 출력한다.
아래와 같은 양식으로 사용자가 입력한 배경과 분위기에 맞는 다른 내용의 게임 시나리오를 출력한다.
사용자가 입력한 게임의 최종 목표나 챕터 목표를 절대로 언급해서는 안 되며, npc 정보 또한 절대로 언급해서는 안 된다.
사용자가 입력한 게임 배경에 대한 정보는 출력을 위한 참고사항이며, 해당 정보들을 바탕으로 사용자 입력값: 뒤에 오는 값에 대한 다음 시나리오 진행한다.
만약 사용자가 장소 이동을 원하는 경우 현재 이동 가능한 장소 목록을 제시한다. 
사용자가 입력한 주요 npc 정보들을 토대로 적절한 시점에 npc를 등장시킨다.

현재 플레이중인 게임의 공간적 배경은 19세기 미국이며 장르는 코스믹호러이다.

** 이 표시 안의 내용은 문맥에 맞게 채운다.
###
Narrator (내레이터):
*게임 스토리 진행 멘트 혹은 플레이어의 선택지 생성*

*필요할 경우 현재 상황에 대한 설명*

*NPC 이름*:
*npc 대사 내용*
###";
    private string system_setting;

    void Awake()
    {
        story_object.text = "";
        if (messages.Count == 0)
        {
            var newMessage = new ChatMessage()
            {
                Role = "system",
                Content = system_prompt
            };
            messages.Add(newMessage);
        }
        // system_setting = "플레이어 이름은 " + player_name 
        //                     + "이야. 조력자 NPC 이름은 "+PNPC_name + "이고," + PNPC_info + @"처럼 행동해야 해. 
        //                     적대자 NPC 이름은 "+ANPC_name + "이고," + ANPC_info + "처럼 행동해야 해";
        // system_setting += "이 게임의 최종 목표는 " + final_title + final_content + "이고 현재 챕터의 목표는 " + chapter_title + chapter_content + "이야.";
    }

    public void SendButton()
    {
        input_msg.Role = "user";
        input_msg.Content = player_input.text;

        AppendMsg(input_msg);
        SendReply();



    }
    void AppendMsg(ChatMessage msg)
    {
        string add_text = "";
        if (msg.Role == "user")
        {
            add_text += PlayerStatManager.playerstat.charname + "> ";
        }

        add_text += msg.Content;

        story_object.text += add_text + "\n\n";
        LayoutRebuilder.ForceRebuildLayoutImmediate(scroll.content);
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scroll.content.sizeDelta.y);
        scroll.verticalNormalizedPosition = 0f;
    }

    private async void SendReply()
    {
        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = player_input.text
        };

        messages.Add(newMessage);

        button.enabled = false;
        player_input.text = "";
        player_input.enabled = false;

        // Complete the instruction
        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo",
            Messages = messages
        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();

            messages.Add(message);
            AppendMsg(message);
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }

        button.enabled = true;
        player_input.enabled = true;
    }
}
