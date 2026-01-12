public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val=0, ListNode next=null)
    {
        this.val = val;
        this.next = next;
    }
}

public class Solution
{

    public ListNode SortListIterative(ListNode head)
    {
        // Base case: empty list or single node is already sorted
        if (head == null || head.next == null)
        {
            return head;
        }

        // get and store the length of the linked list
        int length = getLength(head);
        // create a dummy node that points to the head of the linked list
        ListNode dummy = new ListNode(0, head);
        // set the size of the sublist length to 1 and while subListLength is less than the length of the list iterate over sublist sizes: 1, 2, 4, 8, ...
        for (int subListLength = 1; subListLength < length; subListLength *= 2)
        {
            // initialize current to the start of the list and nextStart to the dummy node
            ListNode current = dummy.next;
            ListNode nextStart = dummy;
            // while current is not null, split the list into subgroups of size 'subListLength' and merge them
            while (current != null)
            {
                // initialize left sublist to current 
                ListNode left = current;
                // split the left subgroup and get the right subgroup
                ListNode right = subgroupSplit(left, subListLength);
                // split the right subgroup and update current to the start of the next pair of sublists
                current = subgroupSplit(right, subListLength);
                // merge the left and right sublists and link them to nextStart
                nextStart.next = Merge(left, right);
                // move nextStart to the end of the merged sublist
                while (nextStart.next != null)
                {
                    nextStart = nextStart.next;
                }
            }
        }
        return dummy.next;
    }

    // helper method to split the linked list into subgroups of given size
    private ListNode subgroupSplit(ListNode head, int size)
    {
        // if head is null, return null
        if (head == null)
        {
            return null;
        }
        // initialize current to head
        ListNode current = head;
        // traverse size-1 nodes or until the end of the list
        for (int i = 1; i < size && current != null; i++)
        {
            current = current.next;
        }
        // if current is null, return null (we reached the end of the list)
        if (current == null)
        {
            return null;
        }
        // store the next node after the subgroup
        ListNode nextSubgroup = current.next;
        // disconnect the subgroup
        current.next = null;
        // return the head of the next subgroup
        return nextSubgroup;
    }
    
    // helper method to get the length of a linked list
    private int getLength(ListNode head)
    {
        // we initialize current to head and length to 0
        ListNode current = head;
        int length = 0;
        // we traverse the list and increment the length until current is null
        while (current != null)
        {
            length++;
            current = current.next;
        }
        // return the number of nodes in the linked list
        return length;
    }
    
    // merge two sorted linked lists
    
    private ListNode Merge(ListNode left, ListNode right)
    {
        ListNode dummy = new ListNode();
        ListNode current = dummy;

        // compare nodes and link them in sorted order
        while (left != null && right != null)
        {
            if (left.val < right.val)
            {
                current.next = left;
                left = left.next;
            }
            else
            {
                current.next = right;
                right = right.next;
            }
            current = current.next;
        }

        // attach remaining nodes (if any)
        if (left != null)
        {
            current.next = left;
        }
        else if (right != null)
        {
            current.next = right;
        }

        return dummy.next;

    }
}

public class Program
{

    // Helper function to print the list (useful for checking before/after sorting)
    public static void PrintList(ListNode head)
    {
        if (head == null)
        {
            Console.WriteLine("List is empty.");
            return;
        }

        ListNode current = head;
        while (current != null)
        {
            Console.Write($"[{current.val}]");
            if (current.next != null)
            {
                Console.Write(" -> ");
            }
            current = current.next;
        }
        Console.WriteLine();
    }

    public static void Main(string[] args)
    {
        // --- 1. Define the input values ---
        int[] values = { -1, 5, 3, 4, 0, -4, 8, 9, 12, -7 };

        // --- 2. Instantiate the Linked List ---
        // Create a Dummy Head or use a simple construction loop (preferred for arrays)

        ListNode dummyHead = new ListNode();
        ListNode current = dummyHead;

        Console.WriteLine("Instantiating Linked List:");

        foreach (int val in values)
        {
            // Create a new node with the current value
            ListNode newNode = new ListNode(val);

            // Link the previous node to this new node
            current.next = newNode;

            // Move the current pointer forward
            current = current.next;
        }

        // The actual head of the list is the next node after the dummy head
        ListNode head = dummyHead.next;

        // --- 3. Print the List to Verify Instantiation ---
        Console.Write("Original List: ");
        PrintList(head); // Output: [ -1 ] -> [ 5 ] -> [ 3 ] -> [ 4 ] -> [ 0 ]

        // --- 4. Call the SortList method ---
        Solution solution = new Solution();
        ListNode sortedHead = solution.SortListIterative(head);

        // --- 5. Print the Sorted List ---
        Console.Write("Sorted List: ");
        PrintList(sortedHead); // Expected Output: [ -1 ] -> [ 0 ] -> [ 3 ] -> [ 4 ] -> [ 5 ]
    }
}


    public class Solution2 {
        public ListNode SortList(ListNode head) {
        
        // Base case: empty list or single node is already sorted
        
            if (head == null || head.next == null) {
                return head;
            }

            // split the list into two halves
        
            ListNode middle = GetMiddle(head);
            ListNode right = middle.next;
        
            // disconnect the two halves
        
            middle.next = null;

            //recursively sort both halves
        
            ListNode left = SortList(head);
            right = SortList(right);

            // Merge the sorted halves
        
            return Merge(left, right);
        }

        // Find the middle of the linked list by using a slow and fast pointer
    
        private ListNode GetMiddle(ListNode head) {
            ListNode slow = head;
            ListNode fast = head.next;

            while (fast != null && fast.next != null) {
                slow = slow.next;
                fast = fast.next.next;
            }

            return slow;
        }

        // merge two sorted linked lists
    
        private ListNode Merge(ListNode left, ListNode right) {
            ListNode dummy = new ListNode();
            ListNode current = dummy;

            // compare nodes and link them in sorted order
        
            while (left != null && right != null) {
                if (left.val < right.val) {
                    current.next = left;
                    left = left.next;
                } else {
                    current.next = right;
                    right = right.next;
                }
                current = current.next;
            }

            // attach remaining nodes (if any)
        
            // current.next = (left != null) ? left : right;
        
            if (left != null) {
                current.next = left;
            } else {
                current.next = right;
            }

            return dummy.next;

        }
    }

public class GetList{
    static List<int> getList(int size)
    {
        List<int> result = new List<int>();
        Random rand = new Random();
        for (int i = 0; i < size; i++)
        {
            result.Add(rand.Next(0, 100));
        }
        return result;
    }
}

