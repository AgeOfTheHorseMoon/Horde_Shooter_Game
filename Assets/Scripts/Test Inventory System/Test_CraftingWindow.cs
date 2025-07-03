using System;
using System.Collections.Generic;
using UnityEngine;

public class Test_CraftingWindow : MonoBehaviour
{
	[Header("References")]
	[SerializeField] Test_CraftingRecipeUI recipeUIPrefab;
	[SerializeField] RectTransform recipeUIParent;
	[SerializeField] List<Test_CraftingRecipeUI> craftingRecipeUIs;

	[Header("Public Variables")]
	public ItemContainer ItemContainer;
	public List<Test_CraftingRecipe> CraftingRecipes;

	public EventHandler<Test_BaseItemSlot> OnPointerEnterEvent;
	public EventHandler<Test_BaseItemSlot> OnPointerExitEvent;

	private void OnValidate()
	{
		Init();
	}

	private void Start()
	{
		Init();

		foreach (Test_CraftingRecipeUI craftingRecipeUI in craftingRecipeUIs)
		{
			craftingRecipeUI.OnPointerEnterEvent += OnPointerEnterEvent;
			craftingRecipeUI.OnPointerExitEvent += OnPointerExitEvent;
		}
	}

	private void Init()
	{
		recipeUIParent.GetComponentsInChildren<Test_CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
		UpdateCraftingRecipes();
	}

	public void UpdateCraftingRecipes()
	{
		for (int i = 0; i < CraftingRecipes.Count; i++)
		{
			if (craftingRecipeUIs.Count == i)
			{
				craftingRecipeUIs.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
			}
			else if (craftingRecipeUIs[i] == null)
			{
				craftingRecipeUIs[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);
			}

			craftingRecipeUIs[i].ItemContainer = ItemContainer;
			craftingRecipeUIs[i].CraftingRecipe = CraftingRecipes[i];
		}

		for (int i = CraftingRecipes.Count; i < craftingRecipeUIs.Count; i++)
		{
			craftingRecipeUIs[i].CraftingRecipe = null;
		}
	}
}
