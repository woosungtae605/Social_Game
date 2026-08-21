using System;
using System.Collections.Generic;
using UnityEngine;

namespace Team.KYR.Scripts
{
    public class BoardManager : MonoBehaviour
    {
        [Header("Post UI")]
        [SerializeField] private Transform postContent;
        [SerializeField] private BoardPostItem postItemPrefab;

        [Header("Initially Unlocked Posts")]
        [SerializeField] private BoardPostSo[] initialPosts;

        private readonly List<BoardPostData> unlockedPosts = new List<BoardPostData>();

        private bool showConceptOnly;

        private void Start()
        {
            for (int i = 0; i < initialPosts.Length; i++)
            {
                if (initialPosts[i] == null)
                    continue;

                TryAddPost(initialPosts[i], DateTime.Now);
            }

            ShowAllPosts();
        }

        public void UnlockPost(BoardPostSo postSo)
        {
            if (!TryAddPost(postSo, DateTime.Now))
                return;

            RefreshPostList();
        }

        public void UnlockPost(BoardPostSo postSo, DateTime createdAt)
        {
            if (!TryAddPost(postSo, createdAt))
                return;

            RefreshPostList();
        }

        public void DeletePost(BoardPostData post)
        {
            if (!unlockedPosts.Remove(post))
                return;

            RefreshPostList();
        }

        public void ToggleConcept(BoardPostData post)
        {
            if (!unlockedPosts.Contains(post))
                return;

            post.ToggleConcept();
            RefreshPostList();
        }

        public void ShowAllPosts()
        {
            showConceptOnly = false;
            RefreshPostList();
        }

        public void ShowConceptPosts()
        {
            showConceptOnly = true;
            RefreshPostList();
        }

        private bool TryAddPost(BoardPostSo postSo, DateTime createdAt)
        {
            if (postSo == null || IsPostUnlocked(postSo))
                return false;

            BoardPostData postData = new BoardPostData(postSo, createdAt);
            unlockedPosts.Add(postData);

            return true;
        }

        private bool IsPostUnlocked(BoardPostSo postSo)
        {
            for (int i = 0; i < unlockedPosts.Count; i++)
            {
                if (unlockedPosts[i].Definition == postSo)
                    return true;
            }

            return false;
        }

        private void RefreshPostList()
        {
            ClearPostList();

            List<BoardPostData> visiblePosts = new List<BoardPostData>();

            for (int i = 0; i < unlockedPosts.Count; i++)
            {
                BoardPostData post = unlockedPosts[i];

                if (!showConceptOnly || post.IsConcept)
                    visiblePosts.Add(post);
            }

            visiblePosts.Sort(CompareByNewestDate);

            for (int i = 0; i < visiblePosts.Count; i++)
            {
                BoardPostItem item = Instantiate(postItemPrefab, postContent);
                item.Setup(visiblePosts[i], this);
            }
        }

        private void ClearPostList()
        {
            for (int i = postContent.childCount - 1; i >= 0; i--)
            {
                GameObject child = postContent.GetChild(i).gameObject;
                child.SetActive(false);
                Destroy(child);
            }
        }

        private static int CompareByNewestDate(BoardPostData first, BoardPostData second)
        {
            return second.CreatedAt.CompareTo(first.CreatedAt);
        }
    }
}