Visitor Design Pattern Is Giving Way To Pattern Matching Expressions!

Zoran Horvat

From <https://www.youtube.com/watch?v=tq5ztZO45-g> 

This YouTube video discusses the Visitor design pattern in C#, 
demonstrating how its traditional implementation using interfaces 
and concrete classes becomes unnecessary with the advent of pattern matching in C#. 
The video contrasts the traditional Visitor approach, which requires 
adding an Accept method to each class in a hierarchy and creating numerous visitor classes, 
with a more modern approach leveraging C#'s pattern matching capabilities. 
This modern approach uses lambda expressions and switch expressions to achieve 
the same functionality without introducing new classes or modifying existing class hierarchies. 
The video highlights the advantages of the pattern-matching approach, 
including reduced code complexity and improved maintainability. 
Ultimately, the video argues that pattern matching effectively renders the traditional 
Visitor pattern obsolete in modern C#.

***
The video explains how the Visitor pattern is used to add operations to a class hierarchy 
without modifying the classes themselves

•
Problem: 
The core problem the Visitor pattern solves, is adding new operations to 
a class hierarchy without modifying the classes in that hierarchy. 

In the example given in the video, there's a class hierarchy representing different 
types of personal names (SimpleName, Mononym, CompoundName) and the goal is to format 
these names in various ways when they are used as the author of a publication. 
Without the Visitor pattern, this would require adding formatting methods directly 
into these classes or the Publication class.



•
Traditional Approach: Without the Visitor pattern, the Publication class might have to handle the formatting of author names directly. This means, that for every way that the author's name needs to be formatted, you would need a new method in the Publication class, and every method would need to handle the different name types. This could lead to a complex Publication class, with many methods, each handling slightly different formatting variations.

•
Code Explosion: Directly adding formatting logic to each class in the hierarchy or to the Publication class, could lead to an "explosion" of methods. For each new formatting requirement you would need to add more and more methods.


چندین کلاس داریم