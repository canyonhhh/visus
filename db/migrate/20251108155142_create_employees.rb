class CreateEmployees < ActiveRecord::Migration[8.1]
  def change
    create_table :employees do |t|
      t.references :business, null: false, foreign_key: true
      t.string :name
      t.string :email
      t.integer :role, default: 0, null: false
      t.string :password_digest

      t.timestamps
    end
  end
end
