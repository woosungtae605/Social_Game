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

        [Header("Boards")]
        [SerializeField] private BoardSo[] boards;

        private readonly List<BoardPostData> unlockedPosts = new List<BoardPostData>();

        private BoardSo currentBoard;
        private bool showConceptOnly;

        public BoardSo[] Boards
        {
            get
            {
                if (boards == null)
                    return new BoardSo[0];

                return boards;
            }
        }

        private void Awake()
        {
            for (int i = 0; i < Boards.Length; i++)
            {
                BoardSo board = Boards[i];
                if (board == null)
                    continue;

                BoardPostSo[] posts = board.InitialPosts;
                for (int j = 0; j < posts.Length; j++)
                    TryAddPost(board, posts[j], DateTime.Now);
            }
        }

        public void SelectBoard(BoardSo board)
        {
            currentBoard = board;
            showConceptOnly = false;
            RefreshPostList();
        }

        public void UnlockPost(BoardSo board, BoardPostSo postSo)
        {
            if (!TryAddPost(board, postSo, DateTime.Now))
                return;

            RefreshPostList();
        }

        public void UnlockPost(BoardSo board, BoardPostSo postSo, DateTime createdAt)
        {
            if (!TryAddPost(board, postSo, createdAt))
                return;

            RefreshPostList();
        }

        public void UnlockPost(BoardPostSo postSo)
        {
            BoardSo board = FindBoardForPost(postSo);
            if (board == null)
                return;

            UnlockPost(board, postSo);
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

        private bool TryAddPost(BoardSo board, BoardPostSo postSo, DateTime createdAt)
        {
            if (board == null || postSo == null || IsPostUnlocked(board, postSo))
                return false;

            BoardPostData postData = new BoardPostData(board, postSo, createdAt);
            unlockedPosts.Add(postData);
            return true;
        }

        private bool IsPostUnlocked(BoardSo board, BoardPostSo postSo)
        {
            for (int i = 0; i < unlockedPosts.Count; i++)
            {
                BoardPostData post = unlockedPosts[i];
                if (post.Board == board && post.Definition == postSo)
                    return true;
            }

            return false;
        }

        private BoardSo FindBoardForPost(BoardPostSo postSo)
        {
            if (postSo == null)
                return null;

            for (int i = 0; i < Boards.Length; i++)
            {
                BoardSo board = Boards[i];
                if (board == null)
                    continue;

                BoardPostSo[] posts = board.InitialPosts;
                for (int j = 0; j < posts.Length; j++)
                {
                    if (posts[j] == postSo)
                        return board;
                }
            }

            return null;
        }

        private void RefreshPostList()
        {
            ClearPostList();

            if (currentBoard == null)
                return;

            List<BoardPostData> visiblePosts = new List<BoardPostData>();

            for (int i = 0; i < unlockedPosts.Count; i++)
            {
                BoardPostData post = unlockedPosts[i];
                if (post.Board != currentBoard)
                    continue;

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
